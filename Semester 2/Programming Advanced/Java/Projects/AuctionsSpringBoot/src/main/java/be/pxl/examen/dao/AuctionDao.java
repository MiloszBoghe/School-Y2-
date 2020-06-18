package be.pxl.examen.dao;

import be.pxl.examen.model.Auction;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface AuctionDao extends JpaRepository<Auction, Long> {
    //save is al standaard geimplementeerd
    Auction findAuctionById(long id);
    //findall is standaard geimplementeerd
}
