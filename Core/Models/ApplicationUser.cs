using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string ProfilePicture { get; set; }
        public string Resume { get; set; }
        public string Coverletter { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string RememberMe { get; set; }
        public DateTime DateCreated { get; set; }

        [Display(Name = "Gender")]
        public int? GenderId { get; set; }
        [ForeignKey("GenderId")]
        public virtual Gender Gender { get; set; }

        [Display(Name = "Country")]
        public int? CountryId { get; set; }
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
        [ForeignKey("StateId")]
        public int? StateId { get; set; }
        public virtual State State { get; set; }

    }
}
