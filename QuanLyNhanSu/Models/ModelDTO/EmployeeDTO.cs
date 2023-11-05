namespace QuanLyNhanSu.Models.ModelDTO
{
    public class EmployeeDTO
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Address { set; get; }
        public string? PhoneNumber { set; get; }
        public string? Email { set; get; }
        public string? DepartmentID { set; get; }
        public string? PositionID { set; get; }
        public DateTime? DateStart { get; set; }
        public int? Gender { set; get; }
        public string? UserID { set; get; }
        public string? Avatar { set; get; }
        public string? UserName { set; get; }
        public List<string>? SkillList { set; get; }
        public string? SalaryID { set; get; }
        public double? SalaryAmount { set; get; }
        public DateTime? SalaryStartDate { set; get; }

    }
}
