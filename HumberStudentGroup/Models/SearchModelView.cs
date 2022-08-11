using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HumberStudentGroup.ADO;

namespace HumberStudentGroup.Models
{
    public class SearchModelView
    {
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}