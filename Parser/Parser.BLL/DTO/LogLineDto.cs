using System;
using System.Collections.Generic;

namespace Parser.BLL.DTO
{
    public class LogLineDto
    {
        public long Id { get; set; }
        public string Host { get; set; }
        public string Route { get; set; }
        public DateTime Date { get; set; }
        public int StatusResult { get; set; }
        public int? BytesSent { get; set; }
        public List<QueryParameterDto> Parameters { get; set; } = new List<QueryParameterDto>();
    }
}
