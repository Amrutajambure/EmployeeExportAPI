namespace EmployeeExportAPI.Models
{
    public class EmployeeFilterModel
    {
        public string Company { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public bool? IsActive { get; set; }
    }
}
