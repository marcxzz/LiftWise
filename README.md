# 📚 LiftWise

LiftWise is a web service designed to simplify the management of gym memberships and reservations. With an intuitive interface, any user can sign up, subscribe and renew subscriptions, as well as book admission to different fitness centers without any difficulty.

> ✨ Elevate your fitness experience

## 📘 Project Info

- **Authors**: [Angioni Marco](https://github.com/Marcxzz), [Lorenzo Ledda](https://github.com/diodoLedd), [Putzu Andrea](https://github.com/andrexswampert), [Sforza Davide](https://github.com/dvsf06)
- **Team name**: Strugas (at I.I.S. "Michele Giua" Cagliari - class 5°A 2024/2025)  
- **Development start date**: 2025/05/01
- **Version of analysis**: 0.1
- **Project status**: in progress

## 🧱 System Analysis

In analyzing the first version of the project, we identified the following entities:

- Users
- Gyms
- Memberships
- Reservations

The **User** is the primary actor in the system: it is assigned a unique system-generated identifier, along with master and login information. Among such information, the email address plays a crucial role because it serves as the login and must therefore be unique across the platform, while first and last name can safely be repeated among distinct profiles. Each time a User subscribes to a **Membership**, a Membership record is created that tracks its validity period. The same User can have multiple Membership of the same type as long as the dates do not overlap (this will be handled by backend logic).

The concept of **Gym** is equally central: each gym receives a unique identifier and maintains descriptive attributes such as name, address, and maximum capacity. The uniqueness constraint applies to the combination of name and address, so as to avoid duplicate master records while still allowing multiple facilities to carry the same name if located in different locations (will be handled by backend logic). Gyms serve as destinations for **Reservations** that Users can make. Each Reservation records date, start time, and end time. To protect usability, the system must prevent duplication: a User is not allowed to book the same time slot for the same Gym twice (it will be handled by the backend logic).

Thus, a User can have multiple Membership over time and multiple Reservations, while a Gymnasium can accommodate many Reservations of different Users in parallel up to the limit of its capacity.

## 🗨️ Conceptual Level: Class Diagram

```Mermaid
    classDiagram
    
    class User {
        - idUser: INT
        - firstName: CHAR 30
        - lastname: CHAR 30
        - taxCode: CHAR 16
        - email: CHAR 320 #UNIQUE#
        - passwordHash: CHAR 100
    }

    class Gym {
        - idGym: INT
        - name: CHAR 100
        - telephone: CHAR 20
        - email: CHAR 320
        - address: CHAR 150
        - maxCapacity: INT
    }

    class Membership {
        - idSubscription: INT
        - startDate: DATE
        - endDate: DATE
    }

    class Reservation {
        - idReservation: INT
        - reservationDate: DATE
        - startTime: TIME
        - endTime: TIME
    }

    User "1" -- "N" Membership : Subscribe
    Membership "N" -- "1" Gym : Offer
    Gym "1" -- "N" Reservation : Receive
    Reservation "N" -- "1" User : Make
```

## 🧰 Technologies Used

- ⚙️ **Blazor Server**:
we chose Blazor Server to implement the UI in full-stack C# mode. Blazor Server allows UI components to be rendered on the server, while maintaining a real-time SignalR connection with the browser: this makes the app highly responsive, with instant state updates without reloading the entire page. With Blazor Server, we directly share model classes and logic between frontend and backend, reducing code duplication and simplifying maintenance. Hosting on Azure App Service ensures automatic scalability and out-of-the-box security, while built-in Dependency Injection support facilitates testing and modular development.
- 🎨 **Bootstrap 5**:
to ensure a modern, responsive and consistent interface across all components, we integrated Bootstrap 5 into the design. Thanks to its grid system and utility classes, each adaptive LiftWise page scales perfectly on desktop, tablet and smartphone devices without the need to rewrite custom styles. The default components have been extended with a few custom classes to maintain a clean, professional style. With the intengrated JavaScript package, component animations can be handled without burdening the weight of the web app.
- 🗃️ **SQLite**:
we opted for SQLite as the database for the development environment and local demos because it requires zero server-side configuration, takes up very few resources, and offers surprisingly high performance for small- to medium-scale applications. In an eventual transition to SQL Server on Azure, the transition of the data model and queries is almost transparent.
- 🌳 **GitHub**:
we use GitHub as the main repository for code, facilitating system cooperation and versioning.

## 🗒️ To Do List del progetto

- **version 0.1** (2025/05/01 → 2025/05/10):
  - [x] **Define `Users` scheme**.
    - Create `tblUsers` table with columns:
      - `idUser` INT PK AI
      - `firstName` CHAR(30)
      - `lastName` CHAR(30)
      - `taxCode` CHAR(16)
      - `email` CHAR(320) UNIQUE
      - `passwordHash` CHAR(100)
  - [ ] **Implement registration and authentication**
    - **Signup** with field validation, verifying uniqueness of `email` and calculation of `passwordHash` (encrypted password)
    - **Login** verifying `email` and `passwordHash`.
  - [x] **Define `Gym` scheme**.
    - Create table `tblGyms` with columns:
      - `idGym` INT PK AI
      - `name` CHAR(100)
      - `telephone` CHAR(20)
      - `email` CHAR(320)
      - `address` CHAR(150)
      - `maxCapacity` INT
    - Add unique constraint on (`name`, `address`).
  - [x] **Define `Membership` scheme**.
    - Create table `tblMemberships` with columns:
      - `idMembership` INT PK AI
      - `userId` INT FK → `tblUsers.idUser`
      - `gymId` INT FK → `tblGyms.idGym`
      - `startDate` DATE
      - `endDate` DATE
  - [x] **Membership Logic**
    - Manage subscription membership: check that for the same `idUser` there are no active `Membership` with overlapping periods
    - Manage membership status.
  - [x] **Define `Reservation` scheme**.
    - Create `tblReservations` table with columns:
      - `idReservation` INT PK
      - `gymId` INT FK → `tblGyms.idGym`
      - `userId` INT FK → `tblUsers.idUser`
      - `reservationDate` DATE
      - `startTime` TIME
      - `endTime` TIME
  - [x] **Reservation Logic**
    - Manage reservations:
      - Check that the same `idUser` does not already have a `Reservation` in the same `startTime` for the same `idGym`
      - Check that the number of active reservations in that period does not exceed `tblGyms.maxCapacity`
    - Manage CRUD
  - [x] **Documentation**
    - Update README file

## 💾 DDL

```SQL
CREATE TABLE tblUsers (
    idUser INTEGER PRIMARY KEY AUTOINCREMENT,
    firstName CHAR(30) NOT NULL,
    lastName CHAR(30) NOT NULL,
    taxCode CHAR(16) NOT NULL,
    email CHAR(320) NOT NULL UNIQUE,
    passwordHash CHAR(100) NOT NULL
);

CREATE TABLE tblGyms (
    idGym INTEGER PRIMARY KEY AUTOINCREMENT,
    name CHAR(100) NOT NULL,
    telephone CHAR(20),
    email CHAR(320),
    address CHAR(150) NOT NULL,
    maxCapacity INTEGER NOT NULL,
    UNIQUE(name, address)
);

CREATE TABLE tblMemberships (
    idMembership INTEGER PRIMARY KEY AUTOINCREMENT,
    userId INTEGER NOT NULL,
    gymId INTEGER NOT NULL,
    startDate DATE NOT NULL,
    endDate DATE NOT NULL,
    FOREIGN KEY (userId) REFERENCES tblUsers(idUser),
    FOREIGN KEY (gymId) REFERENCES tblGyms(idGym)
);

CREATE TABLE tblReservations (
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
```
