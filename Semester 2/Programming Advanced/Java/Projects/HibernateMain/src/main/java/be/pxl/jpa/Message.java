package be.pxl.jpa;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;

@Entity
public class Message {
    @Id
    @GeneratedValue
    private long id;

    @Column
    private String text;

    public Message() {

    }

    public Message(String text) {
        this.text = text;
    }


    public void setText(String text) {
        this.text = text;
    }
}
