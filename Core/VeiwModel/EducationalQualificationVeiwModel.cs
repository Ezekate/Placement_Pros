using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.VeiwModel
{
    public class EducationalQualificationVeiwModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FieldOfStudy { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Degree { get; set; }
        public string Grade { get; set; }

        
    }
}
