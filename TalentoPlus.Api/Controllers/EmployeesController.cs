using _1_Application.Services;
using _2_Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalentoPlus.Web.Services;

namespace TalentoPlus.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EmployeesController : ControllerBase
{
    private readonly EmployeeService _service;


    public EmployeesController(EmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _service.GetAllAsync();
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var employee = await _service.GetByIdAsync(id);
        if (employee == null) return NotFound();
        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Employee model)
    {
        await _service.AddAsync(model);
        return Ok(model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Employee model)
    {
        var current = await _service.GetByIdAsync(id);
        if (current == null) return NotFound();

        current.Document = model.Document;
        current.FirstName = model.FirstName;
        current.LastName = model.LastName;
        current.BirthDate = model.BirthDate;
        current.Address = model.Address;
        current.Phone = model.Phone;
        current.Email = model.Email;
        current.JobTitle = model.JobTitle;
        current.Salary = model.Salary;
        current.HireDate = model.HireDate;
        current.Status = model.Status;
        current.EducationLevel = model.EducationLevel;
        current.ProfessionalProfile = model.ProfessionalProfile;
        current.DepartmentId = model.DepartmentId;

        await _service.UpdateAsync(current);

        return Ok(current);
    }
    



    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}