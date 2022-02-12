using System.ComponentModel.DataAnnotations;

namespace UserAPI.Model
{
    /// <summary>
    /// User Response model
    /// </summary>
    public class PostUserRequestModel
    { 
        public string FirstName { get; set;}        
        public string MiddleName { get; set;}        
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }        
        public string EmailAddress { get; set; }
    }
}
