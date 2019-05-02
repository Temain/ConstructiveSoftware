using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ConstructiveSoftware.Services;
using ConstructiveSoftware.Services.Models;
using ConstructiveSoftware.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstructiveSoftware.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AreasController : ControllerBase
	{
		private readonly AreaService _areaService;

		public AreasController(AreaService areaService)
		{
			_areaService = areaService;
		}

		// GET: api/Areas
		[HttpGet]
		public async Task<ActionResult<IEnumerable<VmArea>>> GetAreas(string searchPattern, CancellationToken cancellationToken)
		{
			var areas = await _areaService.GetAreas(a => a.Name.Contains(searchPattern))
				.ToListAsync(cancellationToken);

			var vmAreas = Mapper.Map<IEnumerable<VmArea>>(areas);

			return Ok(vmAreas);
		}

		// GET: api/Areas/5
		[HttpGet("{id}")]
		public async Task<ActionResult<VmArea>> GetArea(int id, CancellationToken cancellationToken)
		{
			var area = await _areaService.GetAreas(a => a.Id == id)
				.SingleOrDefaultAsync(cancellationToken);
			if (area == null)
			{
				return NotFound();
			}

			return Ok(area);
		}

		// PUT: api/Areas/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutArea(int id, AreaView area, CancellationToken cancellationToken)
		{
			if (id != area.Id)
			{
				return BadRequest();
			}

			var areaView = await _areaService.GetAreas(a => a.Id == id)
				.SingleOrDefaultAsync(cancellationToken);
			if (areaView == null)
			{
				return NotFound();
			}

			try
			{
				await _areaService.UpdateAreaAsync(areaView, cancellationToken);
				await _areaService.CommitAsync(cancellationToken);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await AreaExists(id, cancellationToken))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Areas
		[HttpPost]
		public async Task<ActionResult<VmArea>> PostArea(AreaView area, CancellationToken cancellationToken)
		{
			var dbArea = await _areaService.AddAreaAsync(area, cancellationToken);
			await _areaService.CommitAsync(cancellationToken);

			var areaId = dbArea.Id;
			var areaView = await _areaService.GetAreas(a => a.Id == areaId)
				.SingleOrDefaultAsync(cancellationToken);
			if (areaView == null)
			{
				return NotFound();
			}

			var vmArea = Mapper.Map<VmArea>(areaView);

			return CreatedAtAction("GetArea", new { id = areaId }, vmArea);
		}

		// DELETE: api/Areas/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<VmArea>> DeleteArea(int id, CancellationToken cancellationToken)
		{
			var area = await _areaService.GetAreas(a => a.Id == id)
				.SingleOrDefaultAsync(cancellationToken);
			if (area == null)
			{
				return NotFound();
			}

			var areaView = await _areaService.DeleteAreaAsync(id, cancellationToken);
			await _areaService.CommitAsync(cancellationToken);

			var vmArea = Mapper.Map<VmArea>(area);

			return vmArea;
		}

		private async Task<bool> AreaExists(int id, CancellationToken cancellationToken)
		{
			return await _areaService.GetAreas()
				.AnyAsync(e => e.Id == id, cancellationToken);
		}
	}
}
