using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfieAWookie.Core.Selfies.Infrastructures.Data
{
    public class SelfiesContextFactory : IDesignTimeDbContextFactory<SelfiesContext>
    {



        #region Public methods



        public SelfiesContext CreateDbContext(string[] args)
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(),"Settings","appsettings.json"));

            IConfigurationRoot configurationRoot = configurationBuilder.Build();



            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();

            builder.UseSqlServer(configurationRoot.GetConnectionString("SelfiesDatabase"));
               // b => b.MigrationsAssembly("SelfieAWookie.Core.Data.Migrations"));

            SelfiesContext context = new SelfiesContext(builder.Options);


            return context;
        }



        #endregion

    }
}
