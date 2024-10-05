namespace MyOrganization.DataModel
{
    public class Users
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }            
        public bool IsActive { get; set; }
        public string Password { get; set; }
    }
}
