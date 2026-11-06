namespace agristeam.web.Models.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation property linking to Users
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
