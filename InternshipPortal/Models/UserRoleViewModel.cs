using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternshipPortal.Models
{
    public class UserRoleViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public MultiSelectList Roles { get; set; }
        public  string[] SelectedRoles { get; set; }
    }
}