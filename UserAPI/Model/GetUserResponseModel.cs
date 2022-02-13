namespace UserAPI.Model
{
    
    public class GetUserResponseModel
    {
        public string UserId { get; set; }  
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        
        private string _phone;
        public string PhoneNumber
        { 
            get 
            { 
                return string.Format("{0:###-###-####}", Int64.Parse(this._phone));
            }
            set 
            { 
                _phone = value;
            }
        }        
    }
}
