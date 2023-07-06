using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Metadata;
using BasicRedisLeaderboardDemoDotNetCore.BLL.Entities;

namespace BasicRedisLeaderboardDemoDotNetCore.BLL.DbContexts.EntityConfigurations
{
	public class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<RankEntity>
    {
        public void Configure(EntityTypeBuilder<RankEntity> builder)
        {
            builder.ToTable("company");

            builder.HasKey(x => x.Symbol);

            builder.Property(x => x.Symbol).HasMaxLength(25);

            builder.Property(x => x.Company)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.Country)
                .HasMaxLength(50)
                .IsRequired(false);
            
            builder.Property(x => x.Rank)
                .IsRequired(false);

            builder.Property(x => x.MarketCap)
                .IsRequired(false);
        }
	}
}

