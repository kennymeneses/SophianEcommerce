using Ecommerce.DataAccess;
using Ecommerce.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Tests
{
    public class DataContextInitializer
    {
        public static DataContext GetContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                            .UseInMemoryDatabase(databaseName: "ContextInMemory")
                            .Options;

            var context = new DataContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }

        private static DataContext InitializeData(DataContext context)
        {
                return context;
        }
    }
}
