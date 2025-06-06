USE master;
GO

CREATE DATABASE LNE_Security_ERP;
GO

USE LNE_Security_ERP;
GO

CREATE TABLE CompanyDatabase (
    CompanyId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Street NVARCHAR(255) NULL,
    StreetNumber NVARCHAR(50) NULL,
    City NVARCHAR(100) NULL,
    Country NVARCHAR(100) NULL,
    PostCode NVARCHAR(20) NULL,
    Currency NVARCHAR(3) NOT NULL CHECK (Currency IN ('DKK', 'SEK', 'USD', 'EUR'))
);
GO

CREATE TABLE CustomerDatabase (
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Street NVARCHAR(255) NULL,
    StreetNumber NVARCHAR(50) NULL,
    City NVARCHAR(100) NULL,
    Country NVARCHAR(100) NULL,
    PostCode NVARCHAR(20) NULL,
    Email NVARCHAR(255) NULL,
    Phone NVARCHAR(50) NULL,
    LastPurchaseDate NVARCHAR(20) NULL 
);
GO

CREATE TABLE ProductDatabase (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Price DECIMAL(18, 2) NOT NULL,
    BuyInPrice DECIMAL(18, 2) NOT NULL,
    Quantity DECIMAL(18, 3) NOT NULL,
    Location NVARCHAR(50) NULL,
    Unit NVARCHAR(50) NULL
);
GO

CREATE TABLE SalesDatabase (
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    Created NVARCHAR(20) NOT NULL, 
    OrderCompleted NVARCHAR(20) NULL, 
    OrderCompletedTime NVARCHAR(20) NULL, 
    CustomerId INT NOT NULL,
    State NVARCHAR(50) NOT NULL,
    OrderItems NVARCHAR(MAX) NULL, 

    CONSTRAINT FK_SalesDatabase_Customer FOREIGN KEY (CustomerId) REFERENCES CustomerDatabase(CustomerId)
);
GO
