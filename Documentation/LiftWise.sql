--
-- File generato con SQLiteStudio v3.4.17 su gio mag 1 18:22:15 2025
--
-- Codifica del testo utilizzata: UTF-8
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Tabella: tblGyms
CREATE TABLE IF NOT EXISTS tblGyms (
    idGym INTEGER PRIMARY KEY AUTOINCREMENT,
    name CHAR(100) NOT NULL,
    telephone CHAR(20),
    email CHAR(320),
    address CHAR(150) NOT NULL,
    maxCapacity INTEGER NOT NULL,
    UNIQUE(name, address)
);

-- Tabella: tblReservations
CREATE TABLE IF NOT EXISTS tblReservations (
    idReservation INTEGER PRIMARY KEY AUTOINCREMENT,
    userId INTEGER NOT NULL,
    gymId INTEGER NOT NULL,
    reservationDate DATE NOT NULL,
    startTime TIME NOT NULL,
    endTime TIME NOT NULL,
    FOREIGN KEY (userId) REFERENCES tblUsers(idUser),
    FOREIGN KEY (gymId) REFERENCES tblGyms(idGym),
    UNIQUE(userId, gymId, reservationDate, startTime)
);

-- Tabella: tblMemberships
CREATE TABLE IF NOT EXISTS tblMemberships (
    idMembership INTEGER PRIMARY KEY AUTOINCREMENT,
    userId INTEGER NOT NULL,
    gymId INTEGER NOT NULL,
    startDate DATE NOT NULL,
    endDate DATE NOT NULL,
    FOREIGN KEY (userId) REFERENCES tblUsers(idUser),
    FOREIGN KEY (gymId) REFERENCES tblGyms(idGym)
);

-- Tabella: tblUsers
CREATE TABLE IF NOT EXISTS tblUsers (
    idUser INTEGER PRIMARY KEY AUTOINCREMENT,
    firstName CHAR(30) NOT NULL,
    lastName CHAR(30) NOT NULL,
    taxCode CHAR(16) NOT NULL,
    email CHAR(320) NOT NULL UNIQUE,
    passwordHash CHAR(100) NOT NULL
);

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
