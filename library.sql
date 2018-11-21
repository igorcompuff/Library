CREATE TABLE `authors` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `books` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(100) NOT NULL,
  `Isbn` varchar(50) NOT NULL,
  `Year` varchar(10) NOT NULL,
  `Edition` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Isbn_UNIQUE` (`Isbn`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `subjects` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Subject` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `book_author` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `BookId` int(11) NOT NULL,
  `AuthorId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_BA_BOOK_idx` (`BookId`),
  KEY `FK_BA_AUTHOR_idx` (`AuthorId`),
  CONSTRAINT `FK_BA_AUTHOR` FOREIGN KEY (`AuthorId`) REFERENCES `authors` (`id`),
  CONSTRAINT `FK_BA_BOOK` FOREIGN KEY (`BookId`) REFERENCES `books` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `book_subject` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `BookId` int(11) NOT NULL,
  `SubjectId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_BS_BOOK_idx` (`BookId`),
  KEY `FK_BS_SUBJECT_idx` (`SubjectId`),
  CONSTRAINT `FK_BS_BOOK` FOREIGN KEY (`BookId`) REFERENCES `books` (`id`),
  CONSTRAINT `FK_BS_SUBJECT` FOREIGN KEY (`SubjectId`) REFERENCES `subjects` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
