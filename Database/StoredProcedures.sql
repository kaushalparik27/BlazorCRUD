-- CRUD Stored Procedures for Employee table

-- Create Employee
CREATE PROCEDURE CreateEmployee
    @EmployeeCode NVARCHAR(50),
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email NVARCHAR(100),
    @PhoneNumber NVARCHAR(15),
    @Department NVARCHAR(50),
    @Designation NVARCHAR(50),
    @JoiningDate DATETIME,
    @BasicSalary DECIMAL(10, 2)
AS
BEGIN
    INSERT INTO Employees (EmployeeCode, FirstName, LastName, Email, PhoneNumber, Department, Designation, JoiningDate, BasicSalary)
    VALUES (@EmployeeCode, @FirstName, @LastName, @Email, @PhoneNumber, @Department, @Designation, @JoiningDate, @BasicSalary);
END;

-- Read Employee
CREATE PROCEDURE ReadEmployee
    @EmployeeId INT
AS
BEGIN
    SELECT * FROM Employees WHERE EmployeeId = @EmployeeId;
END;

-- Update Employee
CREATE PROCEDURE UpdateEmployee
    @EmployeeId INT,
    @EmployeeCode NVARCHAR(50),
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email NVARCHAR(100),
    @PhoneNumber NVARCHAR(15),
    @Department NVARCHAR(50),
    @Designation NVARCHAR(50),
    @JoiningDate DATETIME,
    @BasicSalary DECIMAL(10, 2)
AS
BEGIN
    UPDATE Employees
    SET EmployeeCode = @EmployeeCode,
        FirstName = @FirstName,
        LastName = @LastName,
        Email = @Email,
        PhoneNumber = @PhoneNumber,
        Department = @Department,
        Designation = @Designation,
        JoiningDate = @JoiningDate,
        BasicSalary = @BasicSalary
    WHERE EmployeeId = @EmployeeId;
END;

-- Delete Employee
CREATE PROCEDURE DeleteEmployee
    @EmployeeId INT
AS
BEGIN
    DELETE FROM Employees WHERE EmployeeId = @EmployeeId;
END;

-- List All Employees
CREATE PROCEDURE ListEmployees
AS
BEGIN
    SELECT * FROM Employees;
END;