using KargoTakip.Server.Domain.Kargolarim;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KargoTakip.Server.Infrastructure.Configurations;

internal sealed class KargoConfiguration : IEntityTypeConfiguration<Kargo>
{
	public void Configure(EntityTypeBuilder<Kargo> builder)
	{
		builder.OwnsOne(p => p.Gonderen, builder=>
		{
			builder.Property(p => p.FirstName).HasColumnType("varchar(20)");
			builder.Property(p => p.LastName).HasColumnType("varchar(50)");
		});  //tabloma property property birleştiriyor.
		builder.OwnsOne(p => p.Alici);
		builder.OwnsOne(p => p.TeslimAdresi);
		builder.OwnsOne(p=>p.KargoInformation, builder =>
		{
			builder
			.Property(p => p.KargoTipi)
			.HasConversion(tip => tip.Value, value => KargoTipiEnum.FromValue
			(value));
		});
		builder.Property(p => p.KargoDurum).HasConversion(durum => durum.Value, value => KargoDurumEnum.FromValue(value));
	}
}
