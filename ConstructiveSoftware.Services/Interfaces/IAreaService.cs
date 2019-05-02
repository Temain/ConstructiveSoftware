using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ConstructiveSoftware.Domain.Models;
using ConstructiveSoftware.Services.Models;

namespace ConstructiveSoftware.Services.Interfaces
{
	public interface IAreaService
	{
		IQueryable<AreaView> GetAreas(Expression<Func<AreaView, bool>> expr = null);

		Task<Area> AddAreaAsync(AreaView area, CancellationToken cancellationToken);

		Task<Area> UpdateAreaAsync(AreaView area, CancellationToken cancellationToken);

		Task<Area> DeleteAreaAsync(int id, CancellationToken cancellationToken);

		Task<int> CommitAsync(CancellationToken cancellationToken);
	}
}
