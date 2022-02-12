using Cassandra.Mapping.Attributes;

namespace UserAPI.Repository
{
    [Table("users")]
    public class UserEntity
    {
        [PartitionKey]
        [Column("email")]
        public string Email { get; set; }
        
        [Column("user_id")]
        public Guid Id { get; set; }
        
        [Column("first_name")]
        public string FName { get; set; }
        
        [Column("last_name")]
        public string LName { get; set; }
        
        [Column("middle_name")]
        public string MName { get; set; }

        [Column("phone_number")]
        public string Phone { get; set; }
    }
}
