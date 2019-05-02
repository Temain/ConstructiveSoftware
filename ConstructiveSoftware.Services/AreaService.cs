using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ConstructiveSoftware.Domain;
using ConstructiveSoftware.Domain.Models;
using ConstructiveSoftware.Services.Interfaces;
using ConstructiveSoftware.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstructiveSoftware.Services
{
	public class AreaService : IAreaService
	{
		private readonly ApplicationDbContext _context;

		public AreaService(ApplicationDbContext context)
		{
			_context = context;
		}

		public IQueryable<AreaView> GetAreas(Expression<Func<AreaView, bool>> expr = null)
		{
			var query = GetAreasQuery();
			if (expr != null)
			{
				query = query.Where(expr);
			}

			return query;
		}

		public async Task<Area> AddAreaAsync(AreaView area, CancellationToken cancellationToken)
		{
			if (area == null) throw new ArgumentNullException(nameof(area));

			var dbArea = new Area
			{
				Name = area.Name,
				CreatedOn = area.CreatedOn,
				CreatedById = area.CreatedById,
				UpdatedOn = area.UpdatedOn,
				UpdatedById = area.UpdatedById
			};

			await _context.Areas.AddAsync(dbArea, cancellationToken);

			return dbArea;
		}

		public async Task<Area> UpdateAreaAsync(AreaView area, CancellationToken cancellationToken)
		{
			if (area == null) throw new ArgumentNullException(nameof(area));

			var dbArea = await _context.Areas
				.Where(h => h.Id == area.Id)
				.SingleOrDefaultAsync(cancellationToken);

			dbArea.Name = area.Name;
			dbArea.CreatedOn = area.CreatedOn;
			dbArea.CreatedById = area.CreatedById;
			dbArea.UpdatedOn = area.UpdatedOn;
			dbArea.UpdatedById = area.UpdatedById;

			_context.Entry(dbArea).State = EntityState.Modified;

			return dbArea;
		}

		public async Task<Area> DeleteAreaAsync(int id, CancellationToken cancellationToken)
		{
			var dbArea = await _context.Areas
				.SingleOrDefaultAsync(h => h.Id == id, cancellationToken);

			if (dbArea == null) throw new ArgumentNullException(nameof(dbArea));

			_context.Remove(dbArea);

			return dbArea;
		}

		private IQueryable<AreaView> GetAreasQuery()
		{
			var query = _context.Areas
				.Select(a => new AreaView
				{
					Id = a.Id,
					Name = a.Name,
					CreatedOn = a.CreatedOn,
					CreatedById = a.CreatedById,
					UpdatedOn = a.UpdatedOn,
					UpdatedById = a.UpdatedById
				});

			return query;
		}

		public Task<int> CommitAsync(CancellationToken cancellationToken)
		{
			return _context.SaveChangesAsync(cancellationToken);
		}
	}
}
