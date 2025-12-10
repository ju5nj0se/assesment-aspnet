using JuanJoseHernandez.Constants;
using JuanJoseHernandez.Data.Entities;
using JuanJoseHernandez.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuanJoseHernandez.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ICatalogService _catalogService;

        public EmployeesController(IEmployeeService employeeService, ICatalogService catalogService)
        {
            _employeeService = employeeService;
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return View(employees);
        }

        public async Task<IActionResult> Create()
        {
            await LoadCatalogs();
            return View(new User());
        }

        [HttpPost]
        public async Task<IActionResult> Create(User model, string? password)
        {
            if (ModelState.IsValid)
            {
                var result = await _employeeService.CreateEmployeeAsync(model, password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    if (error.Description.Contains("documento"))
                    {
                         ModelState.AddModelError("Document", error.Description);
                    }
                    else
                    {
                         ModelState.AddModelError("", error.Description);
                    }
                }
            }
            await LoadCatalogs();
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _employeeService.GetEmployeeByIdAsync(id);
            if (user == null) return NotFound();

            await LoadCatalogs();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User model)
        {
            if (ModelState.IsValid)
            {
                var result = await _employeeService.UpdateEmployeeAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                     if (error.Description.Contains("documento"))
                    {
                         ModelState.AddModelError("Document", error.Description);
                    }
                    else
                    {
                         ModelState.AddModelError("", error.Description);
                    }
                }
            }
            
            await LoadCatalogs();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DownloadResume(string id)
        {
            var pdfBytes = await _employeeService.GenerateResumePdfAsync(id);
            if (pdfBytes == null) return NotFound();

            var user = await _employeeService.GetEmployeeByIdAsync(id);
            string filename = (user != null) ? $"HojaVida_{user.Names}_{user.LastNames}.pdf" : "HojaVida.pdf";

            return File(pdfBytes, "application/pdf", filename);
        }

        private async Task LoadCatalogs()
        {
            ViewBag.Departments = await _catalogService.GetDepartmentsAsync();
            ViewBag.Degrees = await _catalogService.GetDegreesAsync();
            ViewBag.EducationLevels = await _catalogService.GetEducationLevelsAsync();
            ViewBag.Statuses = await _catalogService.GetStatusesAsync();
        }
    }
}
