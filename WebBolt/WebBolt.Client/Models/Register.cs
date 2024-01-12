namespace WebBolt.Client.Models
{
    public class Register
    {
        public Guid User_Id { get; set; }
        public string? LastName { get; set; }
        public string? FistName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Email { get; set; }
        public byte[] Photo { get; set; } = new byte[0];
    }
}
