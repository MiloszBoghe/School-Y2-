package be.pxl.examen.service;

import be.pxl.examen.dao.AuctionDao;
import be.pxl.examen.dao.UserDao;
import be.pxl.examen.model.Auction;
import be.pxl.examen.model.Bid;
import be.pxl.examen.model.User;
import be.pxl.examen.rest.resource.BidCreateResource;
import be.pxl.examen.util.exception.InvalidBidException;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

import javax.transaction.Transactional;
import java.time.LocalDate;

@SpringBootTest
@Transactional
public class AuctionServiceTest {
    @Autowired
    private AuctionService auctionService;
    @Autowired
    private UserDao userDao;
    @Autowired
    private AuctionDao auctionDao;

    private User user;
    private BidCreateResource bidCreateResource;
    private Auction auction;

    @BeforeEach
    public void beforeEach() {
        user = userDao.findUserById(1);

        //region BidCreateResource aanmaken
        bidCreateResource = new BidCreateResource();
        bidCreateResource.setPrice(50);
        bidCreateResource.setEmail(user.getEmail());
        //endregion

        //region Auction aanmaken
        auction = new Auction();
        auction.setDescription("Testje");
        auction.setEndDate(LocalDate.now().plusDays(20));
        auctionDao.save(auction);
        //endregion
    }


    @Test
    public void doBidThrowsInvalidBidExceptionWhenAuctionDoesNotExist() throws InvalidBidException {
        Auction newAuction = new Auction();

        //Check if the exception thrown is of type 'InvalidBidException'
        Exception e = Assertions.assertThrows(InvalidBidException.class, () -> auctionService.doBid(newAuction.getId(), bidCreateResource));

        //Check if message is right.
        Assertions.assertEquals("Auction does not exist!", e.getMessage());
    }

    @Test
    public void doBidThrowsInvalidBidExceptionWhenNewBidIsLower() {
        //region Test bids aanmaken
        Bid highestBid = bidBuilder(user, LocalDate.now(), 3000.00, auction);
        Bid lowerBid = bidBuilder(user, LocalDate.now(), 200.00, auction);
        //endregion

        //region BidCreateResource invullen
        bidCreateResource.setEmail(user.getEmail());
        bidCreateResource.setPrice(lowerBid.getAmount());
        //endregion

        auction.addBid(highestBid);

        //Check if the exception thrown is of type 'InvalidBidException'
        Exception e = Assertions.assertThrows(InvalidBidException.class, () -> auctionService.doBid(auction.getId(), bidCreateResource));

        //Check if message is right.
        Assertions.assertEquals("Bid is too low!", e.getMessage());
    }

    @Test
    public void doBidThrowsInvalidBidExceptionWhenUserIsAlreadyHighestBidder() {
        //region Test bids aanmaken
        Bid highestBid = bidBuilder(user, LocalDate.now(), 200.00, auction);
        Bid newHighestBid = bidBuilder(user, LocalDate.now(), 300.00, auction);
        //endregion

        //region BidCreateResource invullen
        bidCreateResource.setEmail(user.getEmail());
        bidCreateResource.setPrice(newHighestBid.getAmount());
        //endregion

        auction.addBid(highestBid);

        //Check if the exception thrown is of type 'InvalidBidException'
        Exception e = Assertions.assertThrows(InvalidBidException.class, () -> auctionService.doBid(auction.getId(), bidCreateResource));

        //Check if message is right.
        Assertions.assertEquals("You currently have the highest bid!", e.getMessage());
    }

    @Test
    public void doBidThrowsInvalidBidExceptionWhenAuctionIsFinished() {
        User user2 = userDao.findUserById(2);
        Bid bid = bidBuilder(user2, LocalDate.now(), 500.00, auction);



        auction.addBid(bid);
        auction.setEndDate(LocalDate.now().minusDays(1000));

        bidCreateResource.setPrice(5000);
        //Check if the exception thrown is of type 'InvalidBidException'
        Exception e = Assertions.assertThrows(InvalidBidException.class, () -> auctionService.doBid(auction.getId(), bidCreateResource));

        //Check if message is right.
        Assertions.assertEquals("Auction is finished!", e.getMessage());
    }

    private Bid bidBuilder(User user, LocalDate date, double amount, Auction auction){
        return new Bid(user, date, amount, auction);
    }
}
