using System;
using System.ComponentModel.DataAnnotations;

namespace ConstructiveSoftware.Services.Models
{
	public class AreaView
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(200)]
		public string Name { get; set; }

		public DateTimeOffset CreatedOn { get; set; }

		public int CreatedById { get; set; }

		public DateTimeOffset? UpdatedOn { get; set; }

		public int? UpdatedById { get; set; }
	}
}
