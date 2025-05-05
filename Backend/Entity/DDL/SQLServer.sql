CREATE TABLE Person (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(30) NOT NULL,
    LastName NVARCHAR(30) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    DocumentNumber NVARCHAR(10) NOT NULL,
    Phone NVARCHAR(15) NOT NULL,
    Address NVARCHAR(100) NOT NULL,
    DocumentType CHAR(3) NOT NULL,
    BloodType CHAR(3) NOT NULL,
    Active BIT NOT NULL DEFAULT 1,
    CHECK (DocumentNumber NOT LIKE '%[^0-9]%'),
    CHECK (DocumentType IN ('RC', 'TI', 'CC', 'CE', 'NIT', 'PP')),
    CHECK (BloodType IN ('A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', 'O+', 'O-'))
);

CREATE TABLE [User] (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    Active BIT NOT NULL DEFAULT 1,
    PersonId INT NOT NULL
);


CREATE TABLE Rol (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    Description NVARCHAR(200) NULL,
    Active BIT NOT NULL DEFAULT 1
);

CREATE TABLE UserRole (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Active BIT NOT NULL DEFAULT 1,
    UserId INT NOT NULL,
    RoleId INT NOT NULL
);