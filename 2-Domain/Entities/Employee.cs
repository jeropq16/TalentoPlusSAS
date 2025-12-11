namespace _2_Domain.Entities;

public class Employee
{
    public int Id { get; set; }
    public string Document { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string Address { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string JobTitle { get; set; } = null!;
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
    public string Status { get; set; } = null!;
    public string EducationLevel { get; set; } = null!;
    public string ProfessionalProfile { get; set; } = null!;

    public int DepartmentId { get; set; }
    public Department? Department { get; set; }
}