using System;
using System.Collections.Generic;

namespace Parser.Data.Core.Entities
{
    public class LogLine : BaseEntity
    {
        public string Host { get; set; }
        public string Route { get; set; }
        public DateTime Date { get; set; }
        public int StatusResult { get; set; }
        public int? BytesSent { get; set; }
        public virtual List<QueryParameter> Parameters { get; set; } = new List<QueryParameter>();
    }
}
