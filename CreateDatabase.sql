CREATE SCHEMA IF NOT EXISTS test;
USE test;

CREATE TABLE IF NOT EXISTS kfz (
	id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    fahrgestell TEXT,
    kennzeichen TEXT,
    leistung INT,
    id_typ INT NOT NULL
);

ALTER TABLE `test`.`kfz` 
ADD CONSTRAINT `fk_kfz_typ`
  FOREIGN KEY (`id_typ`)
  REFERENCES `test`.`typ` (`id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

CREATE TABLE IF NOT EXISTS typ (
	id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    beschreibung TEXT
);

INSERT INTO typ VALUES(1, "Sportwagen"), (2, "Limousine"), (3, "Roller");
INSERT INTO kfz VALUES (0, 666, "S-EX-69", 420, 1), (0, 123, "S-AF-19", 29, 3), (0, 555, "B-AM-1945", 100, 2);