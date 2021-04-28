using Microsoft.EntityFrameworkCore;
using SelfieAWookie.Core.Framework;
using SelfieAWookie.Core.Selfies.Domain.Models;
using SelfieAWookie.Core.Selfies.Infrastructures.Data.TypeConfigurations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfieAWookie.Core.Selfies.Infrastructures.Data
{
    public class SelfiesContext : DbContext, IUnitOfWork
    {
        #region Ctor
        //public SelfiesContext([NotNullAttribute] DbContextOptions<SelfiesContext> options ):base(options){}

        public SelfiesContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }
        public SelfiesContext() : base() { }

        #endregion

        #region Internal methods
        protected override    void    OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new SelfieEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WoookieEntityTypeConfiguration());
        }
        #endregion


        #region Properties
        public DbSet<Selfie> Selfies { get; set; }
        public DbSet<Wookie> Wookies { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        #endregion
    }
}
