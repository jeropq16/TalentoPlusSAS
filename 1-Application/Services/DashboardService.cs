using _2_Domain.Interfaces;

namespace _1_Application.Services;

public class DashboardService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;

    public DashboardService(
        IEmployeeRepository employeeRepository,
        IDepartmentRepository departmentRepository)
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task<int> GetTotalEmployeesAsync()
    {
        var list = await _employeeRepository.GetAllAsync();
        return list.Count;
    }

    public async Task<int> GetActiveEmployeesAsync()
    {
        var list = await _employeeRepository.GetAllAsync();
        return list.Count(e => e.Status.ToLower() == "activo");
    }

    public async Task<int> GetInactiveEmployeesAsync()
    {
        var list = await _employeeRepository.GetAllAsync();
        return list.Count(e => e.Status.ToLower() == "inactivo");
    }

    public async Task<Dictionary<string, int>> GetEmployeesByDepartmentAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return employees
            .GroupBy(e => e.Department?.Name ?? "Unknown")
            .ToDictionary(g => g.Key, g => g.Count());
    }
}