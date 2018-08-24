using Microsoft.EntityFrameworkCore;
using NbaStore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NbaStore.Tests.Mocks
{
    public static class MockDbContext
    {
        public static NbaStoreDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<NbaStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new NbaStoreDbContext(options);
        }
    }
}
