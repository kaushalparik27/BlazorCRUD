using BlazorCRUD.Data;
using BlazorCRUD.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorCRUD.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            try
            {
                return await _context.Employees.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all employees.", ex);
            }
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int employeeId)
        {
            try
            {
                return await _context.Employees.FindAsync(employeeId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving employee with id: {employeeId}.", ex);
            }
        }

        public async Task<int> AddEmployeeAsync(Employee employee)
        {
            try
            {
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
                return employee.EmployeeId;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a new employee.", ex);
            }
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                _context.Entry(employee).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the employee.", ex);
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int employeeId)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(employeeId);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting employee with id: {employeeId}.", ex);
            }
        }
    }
}