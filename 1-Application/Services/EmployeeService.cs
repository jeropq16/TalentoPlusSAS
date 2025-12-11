using _2_Domain.Entities;
using _2_Domain.Interfaces;

namespace _1_Application.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public Task<List<Employee>> GetAllAsync()
    {
        return _employeeRepository.GetAllAsync();
    }

    public Task<Employee?> GetByIdAsync(int id)
    {
        return _employeeRepository.GetByIdAsync(id);
    }

    public Task AddAsync(Employee employee)
    {
        return _employeeRepository.AddAsync(employee);
    }

    public Task UpdateAsync(Employee employee)
    {
        return _employeeRepository.UpdateAsync(employee);
    }

    public Task DeleteAsync(int id)
    {
        return _employeeRepository.DeleteAsync(id);
    }
}