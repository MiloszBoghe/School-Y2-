package be.pxl.examen.rest.resource;

import be.pxl.examen.model.Bid;

import java.time.LocalDate;
import java.util.List;

public class AuctionResource {
    private long id;
    private String description;
    private LocalDate endDate;
    private int numbderOfBids;
    private double highestBid;
    private String highestBidBy;
    private boolean finished;

    public AuctionResource(String description, LocalDate endDate) {
        this.description = description;
        this.endDate = endDate;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public LocalDate getEndDate() {
        return endDate;
    }

    public void setEndDate(LocalDate endDate) {
        this.endDate = endDate;
    }


    public int getNumbderOfBids() {
        return numbderOfBids;
    }

    public void setNumbderOfBids(int numbderOfBids) {
        this.numbderOfBids = numbderOfBids;
    }

    public double getHighestBid() {
        return highestBid;
    }

    public void setHighestBid(double highestBid) {
        this.highestBid = highestBid;
    }

    public String getHighestBidBy() {
        return highestBidBy;
    }

    public void setHighestBidBy(String highestBidBy) {
        this.highestBidBy = highestBidBy;
    }

    public boolean isFinished() {
        return finished;
    }

    public void setFinished(boolean finished) {
        this.finished = finished;
    }
}
