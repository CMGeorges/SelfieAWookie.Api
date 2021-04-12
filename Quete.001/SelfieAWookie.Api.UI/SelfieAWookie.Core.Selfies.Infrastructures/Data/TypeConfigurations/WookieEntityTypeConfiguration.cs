using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SelfieAWookie.Core.Selfies.Domain.Models;

namespace SelfieAWookie.Core.Selfies.Infrastructures.Data.TypeConfigurations
{
    class WoookieEntityTypeConfiguration : IEntityTypeConfiguration<Wookie>
    {

        #region Public methods
        public void Configure(EntityTypeBuilder<Wookie> builder)
        {
            builder.ToTable("Wookie");


        }

    
        #endregion
    }
}
