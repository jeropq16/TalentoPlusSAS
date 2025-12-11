using _1_Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentoPlus.Web.Models;

namespace TalentoPlus.Web.Controllers;

public class DashboardController : Controller
{
    private readonly DashboardService _dashboard;
    private readonly EmployeeService _employeeService;
    private readonly DepartmentService _departmentService;

    public DashboardController(DashboardService dashboard, EmployeeService employeeService, DepartmentService departmentService)
    {
        _dashboard = dashboard;
        _employeeService = employeeService;
        _departmentService = departmentService;
    }

    public async Task<IActionResult> Index()
    {
        var employees = await _employeeService.GetAllAsync();
        var departments = await _departmentService.GetAllAsync();

        var model = new DashboardViewModel
        {
            TotalEmployees = employees.Count(),
            TotalDepartments = departments.Count(),
            ActiveEmployees = employees.Count(), // si tienes campo Status, puedes filtrar aquí
            AverageSalary = employees.Any() ? employees.Average(e => e.Salary) : 0,
            LastEmployees = employees
                .OrderByDescending(e => e.Id)
                .Take(5)
                .ToList()
        };

        return View(model);
    }
}