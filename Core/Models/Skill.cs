using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
    public class Skill:BaseModel
    {
     
        [Display(Name = "User")]
        public  string UserId { get; set; }
       [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

    }
}
