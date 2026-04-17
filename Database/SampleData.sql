-- Sample employee data for testing
-- Run this script after creating the Employees table

USE BlazorCRUDDB;
GO

INSERT INTO Employee (EmployeeCode, FirstName, LastName, Email, PhoneNumber, Department, Designation, JoiningDate, BasicSalary)
VALUES
    ('EMP001', 'Alice',   'Johnson',  'alice.johnson@example.com',  '+1-555-0101', 'Engineering',  'Senior Software Engineer', '2019-03-15', 95000.00),
    ('EMP002', 'Bob',     'Smith',    'bob.smith@example.com',      '+1-555-0102', 'Engineering',  'Software Engineer',        '2020-07-01', 75000.00),
    ('EMP003', 'Carol',   'Williams', 'carol.williams@example.com', '+1-555-0103', 'HR',           'HR Manager',               '2018-01-10', 80000.00),
    ('EMP004', 'David',   'Brown',    'david.brown@example.com',    '+1-555-0104', 'Finance',      'Financial Analyst',        '2021-04-20', 70000.00),
    ('EMP005', 'Eva',     'Davis',    'eva.davis@example.com',      '+1-555-0105', 'Marketing',    'Marketing Lead',           '2020-11-05', 72000.00),
    ('EMP006', 'Frank',   'Miller',   'frank.miller@example.com',   '+1-555-0106', 'Engineering',  'DevOps Engineer',          '2022-02-14', 82000.00),
    ('EMP007', 'Grace',   'Wilson',   'grace.wilson@example.com',   '+1-555-0107', 'Design',       'UI/UX Designer',           '2021-09-01', 68000.00),
    ('EMP008', 'Henry',   'Moore',    'henry.moore@example.com',    '+1-555-0108', 'Sales',        'Sales Manager',            '2019-06-23', 85000.00),
    ('EMP009', 'Isabel',  'Taylor',   'isabel.taylor@example.com',  '+1-555-0109', 'HR',           'HR Specialist',            '2023-01-16', 60000.00),
    ('EMP010', 'James',   'Anderson', 'james.anderson@example.com', '+1-555-0110', 'Finance',      'Accountant',               '2022-08-08', 65000.00);
GO
