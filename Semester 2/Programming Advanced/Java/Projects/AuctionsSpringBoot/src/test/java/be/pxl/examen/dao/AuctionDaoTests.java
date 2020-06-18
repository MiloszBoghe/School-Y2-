package be.pxl.examen.dao;

import be.pxl.examen.model.Auction;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

import javax.transaction.Transactional;

@SpringBootTest
//Dit zorgt ervoor dat er een rollback is na elke test
//Deze test runt ook de data.sql in de resources map in de main
@Transactional
public class AuctionDaoTests {
    @Autowired
    private AuctionDao auctionDao;
    private Auction newAuction;

    @BeforeEach
    public void beforeEach(){

    }

    @Test
    public void saveAuctionSavesAuction() {
        int size = auctionDao.findAll().size();
        createAuction();
        int newSize = auctionDao.findAll().size();
        Assertions.assertEquals(++size, newSize);
    }

    @Test
    public void findAuctionByIdReturnsAuction() {
        Auction auction = createAuction();
        Assertions.assertEquals(auction, auctionDao.findAuctionById(auction.getId()));
    }

    private Auction createAuction()
    {
        newAuction = new Auction();
        auctionDao.save(newAuction);
        return newAuction;
    }

}
