using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public bool Active { get; set; }
    }
}
