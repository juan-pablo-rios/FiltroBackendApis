-- Crear tabla 'Owners':
CREATE TABLE Owners (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100),
    LastName VARCHAR(100),
    Email VARCHAR(150) UNIQUE,
    Address VARCHAR(150),
    Phone VARCHAR(25)
); 
-- Insertar información en tabla 'Owners':
INSERT INTO Owners (Name, LastName, Email, Address, Phone) VALUES
('Jaimito', 'Pérez', 'jaimito@gmail.com', 'Calle 69-66', '6666666'),
('Test', 'Test', 'test@gmail.com', 'Calle 12-34', '33333333'),
('Socrates', 'Test', 'socrates@gmail.com', 'Calle 45-78', '99999999');
-- Eliminar tabla:
DROP TABLE Owners;
-- ----------------------------------------------------------------
-- Crear tabla 'Pets':
CREATE TABLE Pets (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(30),
    Specie ENUM('Cat', 'Dog', 'Rabbit', 'Pig', 'Cow'),
    Breed ENUM('Bulldog', 'Labrador', 'Chihuahua', 'Pomeranian', 'Poodle', 'Pitbull'),
    DateBirth DATE,
    Photo TEXT,
    OwnerId INT,
    FOREIGN KEY (OwnerId) REFERENCES Owners(Id)
);
-- Insertar información en tabla 'Pets':
INSERT INTO Pets (Name, Specie, Breed, DateBirth, Photo, OwnerId) VALUES
('Luna', 'Dog', 'Poodle', '2015-05-21', 'Photo', 1),
('Beto', 'Dog', 'Poodle', '2010-10-23', 'Photo', 2),
('Orión', 'Cat', 'Poodle', '2023-11-12', 'Photo', 3),
('Rocky', 'Pig', 'Pitbull', '2014-03-22', 'Photo', 1);
-- Eliminar tabla:
DROP TABLE Pets;
-- ----------------------------------------------------------------
-- Crear tabla 'Appointments':
CREATE TABLE Appointments (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Date DATETIME,
    Description TEXT,
    PetId INT,
    VetId INT,
    FOREIGN KEY (PetId) REFERENCES Pets(Id),
    FOREIGN KEY (VetId) REFERENCES Vets(Id)
);
-- Insertar información en tabla 'Appointments':
INSERT INTO Appointments (Date, Description, PetId, VetId) VALUES
('2023-06-01 10:00:00', 'Cita preventiva', 1, 1),
('2023-06-01 12:00:00', 'Cita de revisión', 2, 2),
('2023-06-02 09:00:00', 'Cita de revisión', 3, 3),
('2023-06-03 13:00:00', 'Cita de preventiva', 4, 3);
-- Eliminar tabla:
DROP TABLE Appointments;
-- ----------------------------------------------------------------
-- Crear tabla 'Vets':
CREATE TABLE Vets (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(120),
    Phone VARCHAR(25),
    Address VARCHAR(150),
    Email VARCHAR(150) UNIQUE
);
-- Insertar información en tabla 'Vets':
INSERT INTO Vets (Name, Phone, Address, Email) VALUES
('Dr. Test', '182738172', 'Calle 69-66','jaimito@gmail.com'),
('Dr. Pacho', '423423413', 'Calle 23-54','pacho@gmail.com'),
('Dr. Prueba', '82937194', 'Calle 12-53','prueba@gmail.com');
-- Eliminar tabla:
DROP TABLE Vets;