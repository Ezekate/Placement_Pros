using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
    public class WorkExperience:BaseModel
    {
        public string WorkPlace { get; set; }
        public string Location { get; set; }
        public string Discription { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateClosed { get; set; }
        public DateTime DateAdded { get; set; }

        [Display(Name = "User")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

    }
}
