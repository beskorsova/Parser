using System;
using System.Collections.Generic;

namespace Parser.BLL.Models
{
    public class LogLine
    {
        public DateTime Date { get; set; }
        public string Host { get; set; }
        public string Route { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public int StatusResult { get; set; }
        public int? BytesSent { get; set; }
        public LogLine()
        {
            this.Parameters = new Dictionary<string, string>();
        }
        public override string ToString()
        {
            return $"{Date} {Host} {Route} {Parameters.Count} {StatusResult} {BytesSent}";
        }
    }
}
