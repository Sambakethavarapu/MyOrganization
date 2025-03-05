namespace MyOrganization.DataModel
{
    public class Users
    {
        public Int32? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsActive { get; set; }
        public string? Password { get; set; }
        public Int32? RoleId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? SurName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
    }
}
