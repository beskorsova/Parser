using System;

namespace Parser.Api.Model.Filters
{
    public class TableFilterModel
    {
        public int Offset { get; set; }
        public int? Limit { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
