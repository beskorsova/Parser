using EmitMapper;
using EmitMapper.MappingConfiguration;
using Parser.BLL.Models;
using Parser.Data.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public static LogLine ToLogLine(this Task<LogLineModel> dto)
        {
            var result = ToLogLineMapper.Map(dto.Result);
            result.Parameters = dto.Result.Parameters.Select(x => new QueryParameter { Name = x.Key, Value = x.Value }).ToList();
            return result;
        }

        public static IEnumerable<LogLine> ToLogLines(this IEnumerable<Task<LogLineModel>> dtos)
        {
            return dtos.Select(x => x.Result.ToLogLine());
        }

        public static IEnumerable<LogLine> ToLogLines(this List<LogLineModel> dtos)
        {
            return dtos.Select(x => x.ToLogLine());
        }
    }
}
