using System.ComponentModel.DataAnnotations;

namespace UserAPI.Model
{
    /// <summary>
    /// User Response model
    /// </summary>
    public class PostUserRequestModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name length can't be more than 50 characters")]
        public string FirstName { get; set;}        
        public string MiddleName { get; set;}        
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Email Address length can't be more than 50 characters")]
        public string EmailAddress { get; set; }
    }
}
