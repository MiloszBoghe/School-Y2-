DROP DATABASE IF EXISTS examenwa2020;

CREATE DATABASE examenwa2020;

USE examenwa2020;

CREATE TABLE product
(
   id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
   name VARCHAR(40) NOT NULL,
   weight FLOAT NOT NULL
);

INSERT INTO `product` (`id`, `name`, `weight`) VALUES
(1, 'handgel',0.12),
(2, 'mondmasker', 0.04),
(3, 'chloroquine tabletten', 0.23);


