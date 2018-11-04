
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Sql;
using Repository.Sql.SqlRepositories;
using Repository.Interface.Interfaces;
using Repository.Common.Specifications;

namespace ClothingStoreApi
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
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddDbContext<SqlContext>(options =>
					options.UseSqlServer(Configuration.GetConnectionString("ClothingStoreDatabase")));

			services.AddTransient<IPurchaseRepository, SqlPurchaseRepository>();
			services.AddTransient<IReturnRepository, SqlReturnRepository>();
			services.AddTransient<IInventoryRepository, SqlInventoryRepository>();
			services.AddTransient<IProductRepository, SqlProductRepository>();
			services.AddTransient<ICreditRepository, SqlCreditRepository>();
			services.AddTransient<IItemRepository, SqlItemRepository>();
			services.AddTransient<IStatisticsRepository, SqlStatisticsRepository>();
			services.AddTransient<IMonthSpecification, MonthSpecification>();
			services.AddTransient<IPolicyManager, PolicyManager>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
