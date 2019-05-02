using AutoMapper;
using ConstructiveSoftware.Services.Models;
using ConstructiveSoftware.WebApi.ViewModels;

namespace ConstructiveSoftware.WebApi.AutoMapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<AreaView, VmArea>();
		}
	}
}
