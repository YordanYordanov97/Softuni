using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NbaStore.App.Mapping;

namespace NbaStore.Tests.Mocks
{
    public static class MockAutoMapper
    {
        static MockAutoMapper()
        {
            Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
        }

        public static IMapper GetAutoMapper() => Mapper.Instance;
    }
}
