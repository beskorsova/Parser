using System;
using System.Collections.Generic;

namespace Parser.Api.Model
{
    public class LogLineModel
    {
        public long Id { get; set; }
        public string Host { get; set; }
        public string Route { get; set; }
        public DateTime Date { get; set; }
        public int StatusResult { get; set; }
        public int? BytesSent { get; set; }
        public string Country { get; set; }
        public virtual List<QueryParameterModel> Parameters { get; set; } = new List<QueryParameterModel>();
    }
}
