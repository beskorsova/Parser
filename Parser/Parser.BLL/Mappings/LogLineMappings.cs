using EmitMapper;
using EmitMapper.MappingConfiguration;
using Parser.BLL.Models;
using Parser.Data.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Parser.BLL.Mappings
{
    static class LogLineMappings
    {
        private static readonly ObjectsMapper<LogLineModel, LogLine> ToLogLineMapper;
        static LogLineMappings()
        {
            DefaultMapConfig config = new DefaultMapConfig().IgnoreMembers<LogLineModel, LogLine>(new[] { nameof(LogLineModel.Parameters) });
            ToLogLineMapper = ObjectMapperManager.DefaultInstance.GetMapper<LogLineModel, LogLine>(config);
        }

        public static LogLine ToLogLine(this LogLineModel dto)
        {
            var result = ToLogLineMapper.Map(dto);
            result.Parameters = dto.Parameters.Select(x => new QueryParameter { Name = x.Key, Value = x.Value }).ToList();
            return result;
        }

        public static List<LogLine> ToLogLines(this IEnumerable<LogLineModel> dtos)
        {
            return dtos.Select(x => x.ToLogLine()).ToList();
        }
    }
}
