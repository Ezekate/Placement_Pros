using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
    public class JobApplications
    {  
        [Key]
        public int Id { get; set; }
        public string Cv { get; set; }
        public string Resume { get; set; }
        public Guid? JobId { get; set; }
        [ForeignKey("JobId")]
        public virtual Job Job { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
