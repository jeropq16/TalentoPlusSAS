using _1_Application.Services;
using _2_Domain.Entities;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TalentoPlus.Web.Models;
using TalentoPlus.Web.Services;

namespace TalentoPlus.Web.Controllers;


[Authorize(Roles = "Admin")] 
public class EmployeesController : Controller
{
    private readonly EmployeeService _employeeService;
    private readonly DepartmentService _departmentService;
    private readonly AIService _aiService;


    public EmployeesController(
        EmployeeService employeeService,
        DepartmentService departmentService,
        AIService aiService)
    {
        _employeeService = employeeService;
        _departmentService = departmentService;
        _aiService = aiService;
    }

    public async Task<IActionResult> Index()
    {
        var employees = await _employeeService.GetAllAsync();
        return View(employees);
    }
    
    private async Task LoadDepartments()
    {
        var departments = await _departmentService.GetAllAsync();
        ViewBag.Departments = new SelectList(departments, "Id", "Name");
    }

    public async Task<IActionResult> Create()
    {
        await LoadDepartments();
        var vm = new EmployeeFormViewModel
        {
            BirthDate = DateTime.Today,
            HireDate = DateTime.Today,
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await LoadDepartments();
            return View(model);
        }

        var employee = new Employee
        {
            Document = model.Document,
            FirstName = model.FirstName,
            LastName = model.LastName,
            BirthDate = model.BirthDate,
            Address = model.Address,
            Phone = model.Phone,
            Email = model.Email,
            JobTitle = model.JobTitle,
            Salary = model.Salary,
            HireDate = model.HireDate,
            Status = model.Status,
            EducationLevel = model.EducationLevel,
            ProfessionalProfile = model.ProfessionalProfile,
            DepartmentId = model.DepartmentId
        };

        await _employeeService.AddAsync(employee);
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Edit(int id)
    {
        var employee = await _employeeService.GetByIdAsync(id);
        if (employee == null) return NotFound();

        var vm = new EmployeeFormViewModel
        {
            Id = employee.Id,
            Document = employee.Document,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            BirthDate = employee.BirthDate,
            Address = employee.Address,
            Phone = employee.Phone,
            Email = employee.Email,
            JobTitle = employee.JobTitle,
            Salary = employee.Salary,
            HireDate = employee.HireDate,
            Status = employee.Status,
            EducationLevel = employee.EducationLevel,
            ProfessionalProfile = employee.ProfessionalProfile,
            DepartmentId = employee.DepartmentId,
            Departments = await GetDepartmentsSelectListAsync()
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, EmployeeFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Departments = await GetDepartmentsSelectListAsync();
            return View(model);
        }

        var employee = await _employeeService.GetByIdAsync(id);
        if (employee == null) return NotFound();

        employee.Document = model.Document;
        employee.FirstName = model.FirstName;
        employee.LastName = model.LastName;
        employee.BirthDate = model.BirthDate;
        employee.Address = model.Address;
        employee.Phone = model.Phone;
        employee.Email = model.Email;
        employee.JobTitle = model.JobTitle;
        employee.Salary = model.Salary;
        employee.HireDate = model.HireDate;
        employee.Status = model.Status;
        employee.EducationLevel = model.EducationLevel;
        employee.ProfessionalProfile = model.ProfessionalProfile;
        employee.DepartmentId = model.DepartmentId;

        await _employeeService.UpdateAsync(employee);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _employeeService.GetByIdAsync(id);
        if (employee == null) return NotFound();

        return View(employee);
    }

    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _employeeService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Import()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Import(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            ModelState.AddModelError("", "Please upload a valid Excel file.");
            return View();
        }

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);

        using var workbook = new XLWorkbook(stream);
        var worksheet = workbook.Worksheets.First();

        var row = 2; // assuming first row is headers

        while (!worksheet.Row(row).IsEmpty())
        {
            var lastName = worksheet.Cell(row, 3).GetString();
            DateTime birthDate;
            worksheet.Cell(row, 4).TryGetValue(out birthDate);           
            var address = worksheet.Cell(row, 5).GetString();
            var phone = worksheet.Cell(row, 6).GetString();
            var email = worksheet.Cell(row, 7).GetString();
            var job = worksheet.Cell(row, 8).GetString();
            var salary = (decimal)worksheet.Cell(row, 9).GetDouble();
            DateTime hireDate;
            worksheet.Cell(row, 10).TryGetValue(out hireDate);            
            var status = worksheet.Cell(row, 11).GetString();
            var education = worksheet.Cell(row, 12).GetString();
            var profile = worksheet.Cell(row, 13).GetString();
            var departmentName = worksheet.Cell(row, 14).GetString();

            var departments = await _departmentService.GetAllAsync();
            var department = departments.FirstOrDefault(d => d.Name == departmentName);

            if (department == null)
            {
                department = new Department { Name = departmentName };
                await _departmentService.AddAsync(department);
            }

            var employee = new Employee
            {
                Document = Guid.NewGuid().ToString().Substring(0, 10), // no hay documento en tu Excel
                FirstName = "N/A",
                LastName = lastName,
                BirthDate = birthDate,
                Address = address,
                Phone = phone,
                Email = email,
                JobTitle = job,
                Salary = salary,
                HireDate = hireDate,
                Status = status,
                EducationLevel = education,
                ProfessionalProfile = profile,
                DepartmentId = department.Id
            };

            await _employeeService.AddAsync(employee);

            row++;
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> ExportPdf()

    {
        var employees = await _employeeService.GetAllAsync();

        using var stream = new MemoryStream();
        using (var document = new Document(PageSize.A4, 25, 25, 30, 30))
        {
            PdfWriter.GetInstance(document, stream);
            document.Open();

            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            var title = new Paragraph("Employees Report", titleFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 20f
            };
            document.Add(title);

            var table = new PdfPTable(6)
            {
                WidthPercentage = 100
            };

            table.SetWidths(new[] { 15f, 20f, 20f, 15f, 15f, 15f });

            // Encabezados
            AddHeaderCell(table, "Document");
            AddHeaderCell(table, "Name");
            AddHeaderCell(table, "Department");
            AddHeaderCell(table, "Job Title");
            AddHeaderCell(table, "Salary");
            AddHeaderCell(table, "Status");

            // Filas
            foreach (var e in employees)
            {
                table.AddCell(e.Document ?? "");
                table.AddCell($"{e.FirstName} {e.LastName}");
                table.AddCell(e.Department?.Name ?? "");
                table.AddCell(e.JobTitle ?? "");
                table.AddCell(e.Salary.ToString("N2"));
                table.AddCell(e.Status ?? "");
            }

            document.Add(table);
            document.Close();
        }

        var bytes = stream.ToArray();
        return File(bytes, "application/pdf", "employees.pdf");
    }

    private void AddHeaderCell(PdfPTable table, string text)
    {
        var font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.White);
        var cell = new PdfPCell(new Phrase(text, font))
        {
            BackgroundColor = new BaseColor(52, 73, 94),
            HorizontalAlignment = Element.ALIGN_CENTER,
            Padding = 5
        };
        table.AddCell(cell);
    }
    
    [HttpPost]
    public async Task<IActionResult> GenerateProfile(string jobTitle, string educationLevel, int departmentId)
    {
        try
        {
            var department = await _departmentService.GetByIdAsync(departmentId);
            string deptName = department?.Name ?? "General";

            var result = await _aiService.GenerateProfileAsync(jobTitle, educationLevel, deptName);

            return Json(new { text = result });
        }
        catch
        {
            return Json(new { text = "No se pudo generar el perfil con IA." });
        }
    }




    private async Task<IEnumerable<SelectListItem>> GetDepartmentsSelectListAsync()
    {
        var departments = await _departmentService.GetAllAsync();
        return departments.Select(d => new SelectListItem
        {
            Value = d.Id.ToString(),
            Text = d.Name
        });
    }
}