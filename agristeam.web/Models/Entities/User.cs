namespace agristeam.web.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User";

        // Navigation property linking to Roles
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
