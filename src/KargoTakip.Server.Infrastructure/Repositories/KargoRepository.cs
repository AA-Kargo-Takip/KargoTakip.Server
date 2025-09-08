using GenericRepository;
using KargoTakip.Server.Domain.Kargolarim;
using KargoTakip.Server.Infrastructure.Context;

namespace KargoTakip.Server.Infrastructure.Repositories
{
	internal sealed class KargoRepository : Repository<Kargo, ApplicationDbContext>, IKargoRespository
	{
		public KargoRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
