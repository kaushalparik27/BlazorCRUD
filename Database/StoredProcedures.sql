-- CRUD Stored Procedures for the Employees table in BlazorCRUD
-- Run this script after creating the Employees table (EmployeeDB.sql)

USE BlazorCRUD;
GO

-- =============================================
-- sp_InsertEmployee: Insert a new employee
-- =============================================
IF OBJECT_ID('dbo.sp_InsertEmployee', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_InsertEmployee;
GO

CREATE PROCEDURE dbo.sp_InsertEmployee
    @EmployeeCode NVARCHAR(50),
    @FirstName    NVARCHAR(50),
    @LastName     NVARCHAR(50),
    @Email        NVARCHAR(100),
    @PhoneNumber  NVARCHAR(15),
    @Department   NVARCHAR(50),
    @Designation  NVARCHAR(50),
    @JoiningDate  DATETIME,
    @BasicSalary  DECIMAL(18,2),
    @NewEmployeeId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Employees (EmployeeCode, FirstName, LastName, Email, PhoneNumber, Department, Designation, JoiningDate, BasicSalary)
    VALUES (@EmployeeCode, @FirstName, @LastName, @Email, @PhoneNumber, @Department, @Designation, @JoiningDate, @BasicSalary);
    SET @NewEmployeeId = SCOPE_IDENTITY();
END;
GO

-- =============================================
-- sp_GetAllEmployees: Retrieve all employees
-- =============================================
IF OBJECT_ID('dbo.sp_GetAllEmployees', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetAllEmployees;
GO

CREATE PROCEDURE dbo.sp_GetAllEmployees
AS
BEGIN
    SET NOCOUNT ON;
    SELECT EmployeeId, EmployeeCode, FirstName, LastName, Email, PhoneNumber,
           Department, Designation, JoiningDate, BasicSalary
    FROM Employees
    ORDER BY EmployeeId;
END;
GO

-- =============================================
-- sp_GetEmployeeById: Get a single employee by ID
-- =============================================
IF OBJECT_ID('dbo.sp_GetEmployeeById', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetEmployeeById;
GO

CREATE PROCEDURE dbo.sp_GetEmployeeById
    @EmployeeId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT EmployeeId, EmployeeCode, FirstName, LastName, Email, PhoneNumber,
           Department, Designation, JoiningDate, BasicSalary
    FROM Employees
    WHERE EmployeeId = @EmployeeId;
END;
GO

-- =============================================
-- sp_UpdateEmployee: Update an existing employee
-- =============================================
IF OBJECT_ID('dbo.sp_UpdateEmployee', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_UpdateEmployee;
GO

CREATE PROCEDURE dbo.sp_UpdateEmployee
    @EmployeeId   INT,
    @EmployeeCode NVARCHAR(50),
    @FirstName    NVARCHAR(50),
    @LastName     NVARCHAR(50),
    @Email        NVARCHAR(100),
    @PhoneNumber  NVARCHAR(15),
    @Department   NVARCHAR(50),
    @Designation  NVARCHAR(50),
    @JoiningDate  DATETIME,
    @BasicSalary  DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Employees
    SET EmployeeCode = @EmployeeCode,
        FirstName    = @FirstName,
        LastName     = @LastName,
        Email        = @Email,
        PhoneNumber  = @PhoneNumber,
        Department   = @Department,
        Designation  = @Designation,
        JoiningDate  = @JoiningDate,
        BasicSalary  = @BasicSalary
    WHERE EmployeeId = @EmployeeId;
END;
GO

-- =============================================
-- sp_DeleteEmployee: Delete an employee by ID
-- =============================================
IF OBJECT_ID('dbo.sp_DeleteEmployee', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_DeleteEmployee;
GO

CREATE PROCEDURE dbo.sp_DeleteEmployee
    @EmployeeId INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Employees WHERE EmployeeId = @EmployeeId;
END;
GO