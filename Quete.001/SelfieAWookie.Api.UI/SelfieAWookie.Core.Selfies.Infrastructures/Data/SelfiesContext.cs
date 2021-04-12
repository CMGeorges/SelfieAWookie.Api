using Microsoft.EntityFrameworkCore;
using SelfieAWookie.Core.Selfies.Domain.Models;
using SelfieAWookie.Core.Selfies.Infrastructures.Data.TypeConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfieAWookie.Core.Selfies.Infrastructures.Data
{
    public class SelfiesContext :DbContext
    {
        #region Internal methods
        protected   override    void    OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new SelfieEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WoookieEntityTypeConfiguration());
        }
        #endregion


        #region Properties
        public DbSet<Selfie> Selfies { get; set; }
        public DbSet<Wookie> Wookies { get; set; }
        #endregion
    }
}
