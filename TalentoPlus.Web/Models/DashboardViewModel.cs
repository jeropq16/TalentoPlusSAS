namespace TalentoPlus.Web.Models;

public class DashboardViewModel
{
    public int TotalEmployees { get; set; }
    public int TotalDepartments { get; set; }
    public int ActiveEmployees { get; set; }
    public decimal AverageSalary { get; set; }
    public List<_2_Domain.Entities.Employee> LastEmployees { get; set; } = new();
}