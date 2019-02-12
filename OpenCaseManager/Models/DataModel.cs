using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCaseManager.Models
{
    public class DataModel
    {
        public string Entity { get; set; }
        public string Type { get; set; }
        public List<Parameter> Parameters { get; set; }
        public List<Filter> Filters { get; set; }
        public List<Order> Order { get; set; }
        public List<string> ResultSet { get; set; }
    }
}