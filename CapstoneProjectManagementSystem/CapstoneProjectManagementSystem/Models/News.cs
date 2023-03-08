using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models
{
    public class News:CommonProperty
    {
        public int NewID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Pin { get; set; }
        public Staff Staff{ get; set; }
    }
}
