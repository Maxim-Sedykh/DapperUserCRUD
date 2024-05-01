namespace DapperUserCRUD.Objects.Entity
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
