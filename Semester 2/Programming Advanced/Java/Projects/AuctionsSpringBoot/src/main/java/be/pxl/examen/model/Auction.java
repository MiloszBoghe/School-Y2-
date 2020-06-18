package be.pxl.examen.model;


import javax.persistence.*;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;

@Entity
public class Auction {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private long id;
    private String description;
    private LocalDate endDate;
    @OneToMany(mappedBy = "auction",
            cascade = CascadeType.ALL,
            fetch = FetchType.EAGER)
    public List<Bid> bids = new ArrayList<>();

    public Auction(String description, LocalDate endDate) {
        this.description = description;
        this.endDate = endDate;
        this.bids = new ArrayList<>();
    }

    public Auction() {
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

    public List<Bid> getBids() {
        return bids;
    }

    //Methods
    public boolean isFinished() {
        return endDate.isBefore(LocalDate.now());
    }

    public void addBid(Bid bid) {
        bids.add(bid);
    }

    public void setBids(List<Bid> bids) {
        this.bids = bids;
    }

    public Bid findHighestBid() {
        if (bids.size() == 0) return null;
        return bids.stream().max(Comparator.comparing(Bid::getAmount)).get();
    }
}
