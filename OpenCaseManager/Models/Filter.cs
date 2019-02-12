using System.Collections.Generic;

namespace OpenCaseManager.Models
{
    public class Filter
    {
        public string Column { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
        public string LogicalOperator { get; set; }
    }
}