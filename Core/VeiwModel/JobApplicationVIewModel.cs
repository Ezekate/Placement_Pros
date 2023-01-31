using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.VeiwModel
{
   public class JobApplicationVIewModel
    {
        public int Id { get; set; }
        public string Cv { get; set; }
        public string Resume { get; set; }
        public Guid? JobId { get; set; }
       
        public string JobTitle { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string CompanyName { get; set; }
        public int Jobcount { get; set; }
       
        
    }
}
