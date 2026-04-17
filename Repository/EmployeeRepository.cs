using BlazorCRUD.Data;
using BlazorCRUD.Helpers;
using BlazorCRUD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BlazorCRUD.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(ApplicationDbContext context, ILogger<EmployeeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            _logger.LogInformation("GetAllEmployees - Fetching all employees from database.");
            var sw = Stopwatch.StartNew();
            try
            {
                var employees = await _context.Employees.ToListAsync();
                sw.Stop();
                _logger.LogInformation("GetAllEmployees - Retrieved {Count} employees in {ElapsedMs}ms.", employees.Count, sw.ElapsedMilliseconds);
                return employees;
            }
            catch (Exception ex)
            {
                sw.Stop();
                _logger.LogError(ex, "GetAllEmployees - Failed to retrieve employees after {ElapsedMs}ms.", sw.ElapsedMilliseconds);
                throw new Exception("An error occurred while retrieving all employees.", ex);
            }
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int employeeId)
        {
            _logger.LogInformation("GetEmployeeById - Fetching employee with ID: {EmployeeId}.", employeeId);
            var sw = Stopwatch.StartNew();
            try
            {
                var employee = await _context.Employees.FindAsync(employeeId);
                sw.Stop();
                if (employee == null)
                {
                    _logger.LogWarning("GetEmployeeById - Employee with ID {EmployeeId} not found. Elapsed: {ElapsedMs}ms.", employeeId, sw.ElapsedMilliseconds);
                }
                else
                {
                    _logger.LogInformation("GetEmployeeById - Employee with ID {EmployeeId} retrieved successfully in {ElapsedMs}ms.", employeeId, sw.ElapsedMilliseconds);
                }
                return employee;
            }
            catch (Exception ex)
            {
                sw.Stop();
                _logger.LogError(ex, "GetEmployeeById - Failed to retrieve employee with ID {EmployeeId} after {ElapsedMs}ms.", employeeId, sw.ElapsedMilliseconds);
                throw new Exception($"An error occurred while retrieving employee with id: {employeeId}.", ex);
            }
        }

        public async Task<int> AddEmployeeAsync(Employee employee)
        {
            var safeCode = SanitizeLogValue(employee.EmployeeCode);
            var safeName = $"{SanitizeLogValue(employee.FirstName)} {SanitizeLogValue(employee.LastName)}";
            _logger.LogInformation("AddEmployee - Adding new employee: Code={EmployeeCode}, Name={FullName}.", safeCode, safeName);
            var sw = Stopwatch.StartNew();
            try
            {
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
                sw.Stop();
                _logger.LogInformation("AddEmployee - Employee added successfully with ID: {EmployeeId} in {ElapsedMs}ms.", employee.EmployeeId, sw.ElapsedMilliseconds);
                return employee.EmployeeId;
            }
            catch (Exception ex)
            {
                sw.Stop();
                _logger.LogError(ex, "AddEmployee - Failed to add employee Code={EmployeeCode} after {ElapsedMs}ms.", safeCode, sw.ElapsedMilliseconds);
                throw new Exception("An error occurred while adding a new employee.", ex);
            }
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            _logger.LogInformation("UpdateEmployee - Updating employee with ID: {EmployeeId}.", employee.EmployeeId);
            var sw = Stopwatch.StartNew();
            try
            {
                _context.Entry(employee).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                sw.Stop();
                _logger.LogInformation("UpdateEmployee - Employee with ID {EmployeeId} updated successfully in {ElapsedMs}ms.", employee.EmployeeId, sw.ElapsedMilliseconds);
                return true;
            }
            catch (Exception ex)
            {
                sw.Stop();
                _logger.LogError(ex, "UpdateEmployee - Failed to update employee with ID {EmployeeId} after {ElapsedMs}ms.", employee.EmployeeId, sw.ElapsedMilliseconds);
                throw new Exception("An error occurred while updating the employee.", ex);
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int employeeId)
        {
            _logger.LogInformation("DeleteEmployee - Deleting employee with ID: {EmployeeId}.", employeeId);
            var sw = Stopwatch.StartNew();
            try
            {
                var employee = await _context.Employees.FindAsync(employeeId);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    await _context.SaveChangesAsync();
                    sw.Stop();
                    _logger.LogInformation("DeleteEmployee - Employee with ID {EmployeeId} deleted successfully in {ElapsedMs}ms.", employeeId, sw.ElapsedMilliseconds);
                    return true;
                }
                sw.Stop();
                _logger.LogWarning("DeleteEmployee - Employee with ID {EmployeeId} not found for deletion. Elapsed: {ElapsedMs}ms.", employeeId, sw.ElapsedMilliseconds);
                return false;
            }
            catch (Exception ex)
            {
                sw.Stop();
                _logger.LogError(ex, "DeleteEmployee - Failed to delete employee with ID {EmployeeId} after {ElapsedMs}ms.", employeeId, sw.ElapsedMilliseconds);
                throw new Exception($"An error occurred while deleting employee with id: {employeeId}.", ex);
            }
        }

        private static string? SanitizeLogValue(string? value)
            => LoggingHelpers.SanitizeLogValue(value);
    }
}