DROP DATABASE IF EXISTS examenwa2019;

CREATE DATABASE examenwa2019;

USE examenwa2019;

CREATE TABLE author
(
   id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
   name varchar(30) NOT NULL
);

CREATE TABLE book
(
   id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
   author_id int NOT NULL,
   title varchar(100) NOT NULL,
   FOREIGN KEY (author_id) REFERENCES author(id)
);


INSERT INTO `author` (`id`, `name`) VALUES
(1, 'tim'),
(2, 'sofie'),
(3, 'jan');


INSERT INTO `book` (`id`, `author_id`, `title`) VALUES
(1, 1, 'Het grote dierenboek'),
(2, 1, 'Het grote plantenboek'),
(3, 2, 'Het prinsessenboek')

