using AutoMapper;
using NbaStore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NbaStore.Services
{
    public abstract class BaseEFService
    {
        protected BaseEFService(
            NbaStoreDbContext dbContext,
            IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }

        protected NbaStoreDbContext DbContext { get; private set; }

        protected IMapper Mapper { get; private set; }
    }
}
