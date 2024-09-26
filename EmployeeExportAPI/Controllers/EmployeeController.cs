using EmployeeExportAPI.Models;
using EmployeeExportAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EmployeeExportAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("Export")]
        public IActionResult Export([FromBody] EmployeeFilterModel filters)
        {
            var employees = _employeeService.GetFilteredEmployees(filters);

            if (employees.Any())
            {
                var excelBytes = _employeeService.ExportToExcel(employees);
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"EmployeeList_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
            }
            else
            {
                return NotFound(new { message = "No employees found with the specified filters." });
            }
        }
    }
}
