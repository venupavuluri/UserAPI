namespace UserAPI.Repository
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string MName { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
