using System;

namespace Parser.Core.Models
{
    public class TableFilterDataModel
    {
        public int Offset { get; set; }
        public int Limit { get; set; } = 10;
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
