using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TalentoPlus.Web.Models;

public class EmployeeFormViewModel
{
    public int? Id { get; set; }

    [Required]
    public string Document { get; set; } = null!;

    [Required]
    [Display(Name = "First name")]
    public string FirstName { get; set; } = null!;

    [Required]
    [Display(Name = "Last name")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Birth date")]
    public DateTime BirthDate { get; set; }

    public string Address { get; set; } = null!;
    public string Phone { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Display(Name = "Job title")]
    public string JobTitle { get; set; } = null!;

    public decimal Salary { get; set; }

    [Display(Name = "Hire date")]
    public DateTime HireDate { get; set; }

    public string Status { get; set; } = null!;

    [Display(Name = "Education level")]
    public string EducationLevel { get; set; } = null!;

    [Display(Name = "Professional profile")]
    public string ProfessionalProfile { get; set; } = null!;

    [Display(Name = "Department")]
    public int DepartmentId { get; set; }

    public IEnumerable<SelectListItem>? Departments { get; set; }
}