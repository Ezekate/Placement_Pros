using System;
using System.Collections.Generic;
using System.Text;

namespace Core.VeiwModel
{
  public class RegisterVeiwModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword{ get; set; }
        public int Gender{ get; set; }
        public string PhoneNo{ get; set; }
    }
}
