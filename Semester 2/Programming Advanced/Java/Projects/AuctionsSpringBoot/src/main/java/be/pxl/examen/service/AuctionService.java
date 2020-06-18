package be.pxl.examen.service;

import be.pxl.examen.dao.AuctionDao;
import be.pxl.examen.dao.UserDao;
import be.pxl.examen.model.Auction;
import be.pxl.examen.model.Bid;
import be.pxl.examen.model.User;
import be.pxl.examen.rest.resource.AuctionCreateResource;
import be.pxl.examen.rest.resource.AuctionResource;
import be.pxl.examen.rest.resource.BidCreateResource;
import be.pxl.examen.util.exception.InvalidBidException;
import org.hibernate.query.criteria.internal.predicate.BetweenPredicate;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class AuctionService {
    private static final DateTimeFormatter DATE_FORMAT = DateTimeFormatter.ofPattern("dd/MM/yyyy");
    @Autowired
    private AuctionDao auctionDao;
    @Autowired
    private UserDao userDao;

    //id is de auctionid, uit de bidCreateResource halen we onze email
    public void doBid(Long id, BidCreateResource bidCreateResource) throws InvalidBidException {
        Auction auction = auctionDao.findAuctionById(id);
        User userWithBid = userDao.findUserByEmail(bidCreateResource.getEmail());

        if (auction == null) throw new InvalidBidException("Auction does not exist!");
        if (userWithBid == null) throw new InvalidBidException("You do not exist!");
        Bid highestBid = auction.findHighestBid();
        if (highestBid == null) highestBid = new Bid(new User(), null, 0.00, auction);
        if (bidCreateResource.getPrice() < highestBid.getAmount()) throw new InvalidBidException("Bid is too low!");

        User UserWithHighestBid = highestBid.getUser();
        if (UserWithHighestBid.getId() == userWithBid.getId())
            throw new InvalidBidException("You currently have the highest bid!");
        if (auction.isFinished()) throw new InvalidBidException("Auction is finished!");

        User user = userDao.findUserByEmail(bidCreateResource.getEmail());
        Bid bid = new Bid(user, LocalDate.now(), bidCreateResource.getPrice(), auction);
        auction.addBid(bid);
        auctionDao.save(auction);
    }

    public List<AuctionResource> findAuctions() {
        return auctionDao.findAll().stream().map(this::mapAuction).collect(Collectors.toList());
    }

    private AuctionResource mapAuction(Auction auction) {
        AuctionResource auctionResource = new AuctionResource(auction.getDescription(), auction.getEndDate());

        Bid highestBid = auction.findHighestBid();
        User highestBidder;
        if (highestBid == null) {
            highestBid = new Bid(new User(), null, 0.00, auction);
            highestBidder = new User();
            auctionResource.setNumbderOfBids(0);
        } else {
            highestBidder = highestBid.getUser();
            auctionResource.setNumbderOfBids(auction.getBids().size());
        }
        auctionResource.setId(auction.getId());
        auctionResource.setFinished(auction.isFinished());
        auctionResource.setHighestBid(highestBid.getAmount());
        auctionResource.setHighestBidBy(highestBidder.getEmail());
        auctionResource.setHighestBid(highestBid.getAmount());
        return auctionResource;
    }

    public AuctionResource createAuction(AuctionCreateResource auctionCreateResource) {
        Auction auction = new Auction(auctionCreateResource.getDescription(), LocalDate.parse(auctionCreateResource.getEndDate(),DATE_FORMAT));
        auctionDao.save(auction);
        return mapAuction(auction);
    }


}
