using SignalRDemo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRDemo.Web.Models
{
    public class IndexViewModel
    {
        public User CurrentUser { get; set; }
        public IEnumerable<Job> Jobs { get; set; }
    }
}
