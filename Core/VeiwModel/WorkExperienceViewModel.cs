using System;
using System.Collections.Generic;
using System.Text;

namespace Core.VeiwModel
{
   public class WorkExperienceViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string WorkPlace { get; set; }
        public string Location { get; set; }
        public string Discription { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime DateClosed { get; set; }
        public DateTime DateAdded { get; set; }
        public bool Deleted { get; set; }
        public bool Active { get; set; }
    }
}
