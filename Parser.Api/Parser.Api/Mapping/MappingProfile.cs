﻿using AutoMapper;
using Parser.Api.Model;
using Parser.Api.Model.Filters;
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
            CreateMap<TopFilterModel, TopFilterDataModel>()
                .ForMember(dest => dest.N, opt => opt.Condition(src => (src.N.HasValue)));
            CreateMap<TableFilterModel, TableFilterDataModel>()
                .ForMember(dest => dest.Limit, opt => opt.Condition(src => (src.Limit.HasValue)));
        }
    }
}
