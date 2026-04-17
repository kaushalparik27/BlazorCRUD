using BlazorCRUD.Helpers;
using BlazorCRUD.Models;
using BlazorCRUD.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BlazorCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeRepository employeeRepository, ILogger<EmployeeController> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            _logger.LogInformation("GET /api/employee - Fetching all employees.");
            var sw = Stopwatch.StartNew();
            try
            {
                var employees = await _employeeRepository.GetAllEmployeesAsync();
                sw.Stop();
                _logger.LogInformation("GET /api/employee - Returned {Count} employees. Status: 200 OK. Elapsed: {ElapsedMs}ms.", employees.Count, sw.ElapsedMilliseconds);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                sw.Stop();
                _logger.LogError(ex, "GET /api/employee - Unhandled exception after {ElapsedMs}ms.", sw.ElapsedMilliseconds);
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            _logger.LogInformation("GET /api/employee/{Id} - Fetching employee.", id);
            var sw = Stopwatch.StartNew();
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
                sw.Stop();
                if (employee == null)
                {
                    _logger.LogWarning("GET /api/employee/{Id} - Employee not found. Status: 404 Not Found. Elapsed: {ElapsedMs}ms.", id, sw.ElapsedMilliseconds);
                    return NotFound($"Employee with ID {id} not found");
                }
                _logger.LogInformation("GET /api/employee/{Id} - Employee found. Status: 200 OK. Elapsed: {ElapsedMs}ms.", id, sw.ElapsedMilliseconds);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                sw.Stop();
                _logger.LogError(ex, "GET /api/employee/{Id} - Unhandled exception after {ElapsedMs}ms.", id, sw.ElapsedMilliseconds);
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee([FromBody] Employee employee)
        {
            var safeCode = SanitizeLogValue(employee?.EmployeeCode);
            _logger.LogInformation("POST /api/employee - Adding new employee: Code={EmployeeCode}.", safeCode);
            var sw = Stopwatch.StartNew();
            try
            {
                if (!ModelState.IsValid)
                {
                    sw.Stop();
                    _logger.LogWarning("POST /api/employee - Validation failed for employee Code={EmployeeCode}. Status: 400 Bad Request. Elapsed: {ElapsedMs}ms.", safeCode, sw.ElapsedMilliseconds);
                    return BadRequest(ModelState);
                }
                var employeeId = await _employeeRepository.AddEmployeeAsync(employee!);
                sw.Stop();
                _logger.LogInformation("POST /api/employee - Employee added successfully with ID: {EmployeeId}. Status: 201 Created. Elapsed: {ElapsedMs}ms.", employeeId, sw.ElapsedMilliseconds);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeId }, employee);
            }
            catch (Exception ex)
            {
                sw.Stop();
                _logger.LogError(ex, "POST /api/employee - Unhandled exception for Code={EmployeeCode} after {ElapsedMs}ms.", safeCode, sw.ElapsedMilliseconds);
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            _logger.LogInformation("PUT /api/employee/{Id} - Updating employee.", id);
            var sw = Stopwatch.StartNew();
            try
            {
                if (id != employee.EmployeeId)
                {
                    sw.Stop();
                    _logger.LogWarning("PUT /api/employee/{Id} - ID mismatch: body ID={BodyId}. Status: 400 Bad Request. Elapsed: {ElapsedMs}ms.", id, employee.EmployeeId, sw.ElapsedMilliseconds);
                    return BadRequest("Employee ID mismatch");
                }
                if (!ModelState.IsValid)
                {
                    sw.Stop();
                    _logger.LogWarning("PUT /api/employee/{Id} - Validation failed. Status: 400 Bad Request. Elapsed: {ElapsedMs}ms.", id, sw.ElapsedMilliseconds);
                    return BadRequest(ModelState);
                }
                var result = await _employeeRepository.UpdateEmployeeAsync(employee);
                sw.Stop();
                if (!result)
                {
                    _logger.LogWarning("PUT /api/employee/{Id} - Employee not found. Status: 404 Not Found. Elapsed: {ElapsedMs}ms.", id, sw.ElapsedMilliseconds);
                    return NotFound($"Employee with ID {id} not found");
                }
                _logger.LogInformation("PUT /api/employee/{Id} - Employee updated successfully. Status: 204 No Content. Elapsed: {ElapsedMs}ms.", id, sw.ElapsedMilliseconds);
                return NoContent();
            }
            catch (Exception ex)
            {
                sw.Stop();
                _logger.LogError(ex, "PUT /api/employee/{Id} - Unhandled exception after {ElapsedMs}ms.", id, sw.ElapsedMilliseconds);
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            _logger.LogInformation("DELETE /api/employee/{Id} - Deleting employee.", id);
            var sw = Stopwatch.StartNew();
            try
            {
                var result = await _employeeRepository.DeleteEmployeeAsync(id);
                sw.Stop();
                if (!result)
                {
                    _logger.LogWarning("DELETE /api/employee/{Id} - Employee not found. Status: 404 Not Found. Elapsed: {ElapsedMs}ms.", id, sw.ElapsedMilliseconds);
                    return NotFound($"Employee with ID {id} not found");
                }
                _logger.LogInformation("DELETE /api/employee/{Id} - Employee deleted successfully. Status: 200 OK. Elapsed: {ElapsedMs}ms.", id, sw.ElapsedMilliseconds);
                return Ok("Employee deleted successfully");
            }
            catch (Exception ex)
            {
                sw.Stop();
                _logger.LogError(ex, "DELETE /api/employee/{Id} - Unhandled exception after {ElapsedMs}ms.", id, sw.ElapsedMilliseconds);
                return BadRequest($"Error: {ex.Message}");
            }
        }

        private static string? SanitizeLogValue(string? value)
            => LoggingHelpers.SanitizeLogValue(value);
    }
}