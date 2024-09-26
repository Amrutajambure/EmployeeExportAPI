using System;

namespace EmployeeExportAPI.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public DateTime DOB { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public bool IsActive { get; set; }
        public string ReportId { get; set; }
    }
}
