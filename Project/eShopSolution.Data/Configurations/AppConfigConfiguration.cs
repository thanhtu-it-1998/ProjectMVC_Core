using Microsoft.EntityFrameworkCore.Metadata.Builders;
using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace eShopSolution.Data.Configuration
{
    class AppConfigConfiguration:IEntityTypeConfiguration<AppConfig>
    {
       public void Configure(EntityTypeBuilder<AppConfig> builder)
        {
            builder.ToTable("AppConfigs");
            builder.HasKey(x => x.Key);
            builder.Property(x => x.Value).IsRequired(true);
        }
    }
}
