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
            DefaultMapConfig config = new DefaultMapConfig();
            ToLogLineMapper = ObjectMapperManager.DefaultInstance.GetMapper<LogLineDto, LogLine>(config);
        }

        public static LogLine ToLogLine(this LogLineDto dto)
        {
            return ToLogLineMapper.Map(dto);
        }
    }
}
