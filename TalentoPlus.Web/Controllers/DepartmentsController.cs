using _1_Application.Services;
using _2_Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace TalentoPlus.Web.Controllers;

public class DepartmentsController : Controller
{
    private readonly DepartmentService _service;

    public DepartmentsController(DepartmentService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var list = await _service.GetAllAsync();
        return View(list);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Department model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _service.AddAsync(model);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var d = await _service.GetByIdAsync(id);
        if (d == null) return NotFound();
        return View(d);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Department model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _service.UpdateAsync(model);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var d = await _service.GetByIdAsync(id);
        if (d == null) return NotFound();

        return View(d);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}