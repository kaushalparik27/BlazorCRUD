using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorCRUD.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(10)]
        public string EmployeeCode { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Department { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Designation { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime JoiningDate { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Basic salary must be a positive number.")]
        public decimal BasicSalary { get; set; }
    }
}