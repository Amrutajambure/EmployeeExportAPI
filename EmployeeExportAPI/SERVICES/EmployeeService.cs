using EmployeeExportAPI.Models;
using OfficeOpenXml; // Add this to use EPPlus
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace EmployeeExportAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private List<Employee> _employeeData;

        public EmployeeService()
        {
            _employeeData = new List<Employee>
            {
                new Employee { EmployeeID = 1, EmployeeCode = "E001", Name = "John Doe", Company = "XYZ Corporation", Department = "IT", Designation = "Software Engineer", IsActive = true, DOB = new DateTime(1990, 1, 1), MobileNo = "1234567890", EmailId = "john@example.com", ReportId = "R001" },
                new Employee { EmployeeID = 2, EmployeeCode = "E002", Name = "Jane Smith", Company = "XYZ Corporation", Department = "IT", Designation = "QA Engineer", IsActive = false, DOB = new DateTime(1992, 2, 2), MobileNo = "0987654321", EmailId = "jane@example.com", ReportId = "R002" }
            };
        }

        public IEnumerable<Employee> GetFilteredEmployees(EmployeeFilterModel filters)
        {
            var query = _employeeData.AsQueryable();

            if (!string.IsNullOrEmpty(filters.Company))
                query = query.Where(e => e.Company.Equals(filters.Company, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(filters.Department))
                query = query.Where(e => e.Department.Equals(filters.Department, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(filters.Designation))
                query = query.Where(e => e.Designation.Equals(filters.Designation, StringComparison.OrdinalIgnoreCase));

            if (filters.IsActive.HasValue)
                query = query.Where(e => e.IsActive == filters.IsActive.Value);

            return query.ToList();
        }

        public byte[] ExportToExcel(IEnumerable<Employee> employees)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Fix License issue

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employees");

                // Add headers
                worksheet.Cells[1, 1].Value = "EmployeeID";
                worksheet.Cells[1, 2].Value = "EmployeeCode";
                worksheet.Cells[1, 3].Value = "Name";
                worksheet.Cells[1, 4].Value = "Company";
                worksheet.Cells[1, 5].Value = "Department";
                worksheet.Cells[1, 6].Value = "Designation";
                worksheet.Cells[1, 7].Value = "ReportId";
                worksheet.Cells[1, 8].Value = "DOB";
                worksheet.Cells[1, 9].Value = "MobileNo";
                worksheet.Cells[1, 10].Value = "EmailId";
                worksheet.Cells[1, 11].Value = "IsActive";

                // Add employee data
                var rowIndex = 2;
                foreach (var employee in employees)
                {
                    worksheet.Cells[rowIndex, 1].Value = employee.EmployeeID;
                    worksheet.Cells[rowIndex, 2].Value = employee.EmployeeCode;
                    worksheet.Cells[rowIndex, 3].Value = employee.Name;
                    worksheet.Cells[rowIndex, 4].Value = employee.Company;
                    worksheet.Cells[rowIndex, 5].Value = employee.Department;
                    worksheet.Cells[rowIndex, 6].Value = employee.Designation;
                    worksheet.Cells[rowIndex, 7].Value = employee.ReportId;
                    worksheet.Cells[rowIndex, 8].Value = employee.DOB.ToString("yyyy-MM-dd");
                    worksheet.Cells[rowIndex, 9].Value = employee.MobileNo;
                    worksheet.Cells[rowIndex, 10].Value = employee.EmailId;
                    worksheet.Cells[rowIndex, 11].Value = employee.IsActive ? "Active" : "Inactive";

                    rowIndex++;
                }

                return package.GetAsByteArray();
            }
        }
    }
}
