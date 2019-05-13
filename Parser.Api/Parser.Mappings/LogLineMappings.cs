using EmitMapper;

namespace Parser.Mappings
{
    public class LogLineMappings
    {
        private static readonly ObjectsMapper<LogLine, LogLineDataModel> ToLogLineMapper;
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

        public static IEnumerable<LogLine> ToLogLines(this List<LogLineModel> dtos)
        {
            return dtos.Select(x => x.ToLogLine());
        }

    }
}
