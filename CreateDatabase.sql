CREATE SCHEMA IF NOT EXISTS test;
USE test;
CREATE TABLE IF NOT EXISTS kfz (
	idkfz INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    FahrgestellNr TEXT,
    Kennzeichen TEXT,
    Leistung INT,
    Typ TEXT
);
INSERT INTO kfz VALUES (0, 666, "S-EX-69", 420, "Sportwagen"), (0, 123, "S-AF-19", 29, "Roller"), (0, 555, "B-AM-1945", 100, "Limousine");