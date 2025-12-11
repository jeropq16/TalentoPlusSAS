using _2_Domain.Entities;
using _2_Domain.Interfaces;

namespace _1_Application.Services;

public class DepartmentService
{
    private readonly IDepartmentRepository _repository;

    public DepartmentService(IDepartmentRepository repository)
    {
        _repository = repository;
    }
    
    public Task<List<Department>> GetAllAsync() => _repository.GetAllAsync();

    public Task<Department?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

    public Task AddAsync(Department d) => _repository.AddAsync(d);

    public Task UpdateAsync(Department d) => _repository.UpdateAsync(d);

    public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
}