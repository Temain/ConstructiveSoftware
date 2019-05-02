using System;

namespace ConstructiveSoftware.WebApi.ViewModels
{
	public class VmArea
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public DateTimeOffset CreatedOn { get; set; }

		public int CreatedById { get; set; }

		public DateTimeOffset? UpdatedOn { get; set; }

		public int? UpdatedById { get; set; }
	}
}
