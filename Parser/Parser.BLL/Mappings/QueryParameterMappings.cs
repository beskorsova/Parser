using EmitMapper;
using EmitMapper.MappingConfiguration;
using Parser.BLL.DTO;
using Parser.Data.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Parser.BLL.Mappings
{
    static class QueryParameterMappings
    {
        private static readonly ObjectsMapper<QueryParameterDto, QueryParameter> ToQueryParameterDtoMapper;
        static QueryParameterMappings()
        {
            DefaultMapConfig config = new DefaultMapConfig();
            ToQueryParameterDtoMapper = ObjectMapperManager.DefaultInstance.GetMapper<QueryParameterDto, QueryParameter>(config);
        }

        public static List<QueryParameter> ToQueryParameters(this List<QueryParameterDto> dto)
        {
            return dto.Select(x => ToQueryParameterDtoMapper.Map(x)).ToList();
        }
    }
}
