using AutoMapper;
using ConstructiveSoftware.Domain;
using ConstructiveSoftware.Services;
using ConstructiveSoftware.Services.Interfaces;
using ConstructiveSoftware.WebApi.Filters;
using ConstructiveSoftware.WebApi.Mappings;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConstructiveSoftware.WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("AllowSpecificMethods",
					option =>
					{
						option.WithOrigins("http://localhost:8080")
							.AllowAnyMethod()
							.AllowCredentials()
							.AllowAnyHeader();
					});
			});

			var connection = Configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connection
				, b => b.MigrationsAssembly("ConstructiveSoftware.Migrations")));

			// Add application services.
			services.AddTransient<IAreaService, AreaService>();

			// Auto Mapper Configurations
			var mappingConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new MappingProfile());
			});
			var mapper = mappingConfig.CreateMapper();
			services.AddSingleton(mapper);

			// Validation
			services.AddMvc(options =>
			{
				options.Filters.Add(typeof(ValidateModelStateAttribute));
			})
			.AddFluentValidation(fv =>
			{
				fv.RegisterValidatorsFromAssemblyContaining<Startup>();
				fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
			})
			.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseCors("AllowSpecificMethods");

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
