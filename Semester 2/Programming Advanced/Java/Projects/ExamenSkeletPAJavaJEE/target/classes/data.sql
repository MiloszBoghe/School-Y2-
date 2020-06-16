
create table IF NOT EXISTS message
(
	ID BIGINT not null auto_increment,
	TEXT VARCHAR(255) not null,
	primary key (ID)
);

INSERT INTO message (text) VALUES
	('Het is juni.'),
	('Veel succes met de examens!');



