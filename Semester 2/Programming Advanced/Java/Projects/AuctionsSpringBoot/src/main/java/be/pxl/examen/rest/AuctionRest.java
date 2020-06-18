package be.pxl.examen.rest;

import be.pxl.examen.rest.resource.*;
import be.pxl.examen.service.AuctionService;
import be.pxl.examen.util.exception.*;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@RestController
@RequestMapping("rest/auctions")
public class AuctionRest {
    private static final Logger LOGGER = LogManager.getLogger(UserRest.class);

    @Autowired
    private AuctionService auctionService;

    @GetMapping
    public List<AuctionResource> getAllAuctions() {
        return auctionService.findAuctions();
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    public AuctionResource createAuction(@RequestBody AuctionCreateResource auctionCreateResource) {
        return auctionService.createAuction(auctionCreateResource);
    }

    @PostMapping("{id}/bids")
    @ResponseStatus(HttpStatus.CREATED)
    public void doBid(@PathVariable Long id, @RequestBody BidCreateResource bidCreateResource) {
        try {
            auctionService.doBid(id, bidCreateResource);
        } catch (InvalidBidException e) {
            throw new ResponseStatusException(HttpStatus.BAD_REQUEST, e.getMessage());
        }
    }

}
