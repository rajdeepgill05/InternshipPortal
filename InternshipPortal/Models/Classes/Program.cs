using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternshipPortal.Models.Classes
{
    public class Program
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Instructor { get; set; }

        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public Program()
        {
            Students = new HashSet<ApplicationUser>();
        }

        public virtual ICollection<ApplicationUser> Students { get; set; }

    }
}