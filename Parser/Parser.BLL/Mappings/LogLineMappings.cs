using EmitMapper;
using EmitMapper.MappingConfiguration;
using Parser.BLL.DTO;
using Parser.Data.Core.Entities;

namespace Parser.BLL.Mappings
{
    static class LogLineMappings
    {
        private static readonly ObjectsMapper<LogLineDto, LogLine> ToLogLineMapper;
        static LogLineMappings()
        {
            DefaultMapConfig config = new DefaultMapConfig().IgnoreMembers<LogLineDto, LogLine>(new[] { nameof(LogLineDto.Parameters) });
            ToLogLineMapper = ObjectMapperManager.DefaultInstance.GetMapper<LogLineDto, LogLine>(config);
        }

        public static LogLine ToLogLine(this LogLineDto dto)
        {
            var result = ToLogLineMapper.Map(dto);
            result.Parameters = dto.Parameters.ToQueryParameters();
            return result;
        }
    }
}
