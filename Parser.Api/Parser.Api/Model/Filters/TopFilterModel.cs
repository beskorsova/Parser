using System;

namespace Parser.Api.Model.Filters
{
    public class TopFilterModel
    {
        public int N { get; set; } = 10;
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
