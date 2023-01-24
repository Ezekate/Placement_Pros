using System;
using System.Collections.Generic;
using System.Text;

namespace Core.VeiwModel
{
   public class PersonalInfoViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int GendeId { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }
        public string Resume { get; set; }
        public string Coverletter { get; set; }
        public string Address { get; set; }
        public string Discription { get; set; }
        public DateTime Birthdate { get; set; }
        public string State { get; set; }
        public int StateId { get; set; }
        public string Country { get; set; }
        public int CountryId { get; set; }
    }
}

