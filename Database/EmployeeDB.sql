-- SQL script to create Employees table for BlazorCRUD application
-- Run this script to create the database and table before starting the application

-- Create the database (if it does not already exist)
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'BlazorCRUDDB')
BEGIN
    CREATE DATABASE BlazorCRUDDB;
END
GO

USE BlazorCRUDDB;
GO

-- Create the Employees table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'Employees')
BEGIN
    CREATE TABLE Employees (
        EmployeeId   INT           PRIMARY KEY IDENTITY(1,1),
        EmployeeCode NVARCHAR(50)  NOT NULL,
        FirstName    NVARCHAR(50)  NOT NULL,
        LastName     NVARCHAR(50)  NOT NULL,
        Email        NVARCHAR(100) NOT NULL,
        PhoneNumber  NVARCHAR(15)  NOT NULL,
        Department   NVARCHAR(50)  NOT NULL,
        Designation  NVARCHAR(50)  NOT NULL,
        JoiningDate  DATETIME      NOT NULL,
        BasicSalary  DECIMAL(18,2) NOT NULL,

        CONSTRAINT UQ_Employees_EmployeeCode UNIQUE (EmployeeCode),
        CONSTRAINT UQ_Employees_Email        UNIQUE (Email)
    );

    -- Non-clustered indexes for frequently searched columns
    CREATE NONCLUSTERED INDEX IX_Employees_Department  ON Employees (Department);
    CREATE NONCLUSTERED INDEX IX_Employees_Designation ON Employees (Designation);
END
GO