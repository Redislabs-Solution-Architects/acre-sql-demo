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
        }
	}
}

