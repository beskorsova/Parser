using AutoMapper;
using Parser.Api.Model;
using Parser.Core.Models;
using Parser.Data.Core.Entities;

namespace Parser.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMapping();
        }

        private void CreateMapping()
        {
            CreateMap<LogLine, LogLineDataModel>();
            CreateMap<LogLineDataModel, LogLineModel>();
        }
    }
}
