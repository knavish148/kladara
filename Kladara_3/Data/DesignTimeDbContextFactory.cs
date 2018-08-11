using Kladara3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kladara3.Data;

namespace Kladara3.Models
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Kladara3Context>
    {
        public Kladara3Context CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<Kladara3Context>();
            var connectionString = configuration.GetConnectionString("Kladara_3Context");
            builder.UseSqlServer(connectionString);
            return new Kladara3Context(builder.Options);
        }
    }
}
