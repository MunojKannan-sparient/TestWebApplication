namespace TestWebApplication.Models.ViewModels
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public bool AdminRoleCheckbox { get; set; }
    }
}
