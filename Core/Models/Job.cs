using Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Job
    {

        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Salary { get; set; }
        public string Discription { get; set; }
        public JobTypeEnum  JobType { get; set; }
        public bool Active { get; set; }
        public string Requirement { get; set; }
        public string CompanyLogo { get; set; }
    }
}
