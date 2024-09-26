using EmployeeExportAPI.Models;
using System.Collections.Generic;

namespace EmployeeExportAPI.Services
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetFilteredEmployees(EmployeeFilterModel filters);
        byte[] ExportToExcel(IEnumerable<Employee> employees);
    }
}
