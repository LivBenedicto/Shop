using Microsoft.AspNetCore.Builder; // apps constructions
using Microsoft.AspNetCore.Hosting; // application self-hosting
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Data;

namespace Shop
{
    public class Startup
    {
        // initializer
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // services.AddDbContext<DataContext>(option => option.UseInMemoryDatabase("Database"));
            services.AddDbContext<DataContext>(
                option => option.UseSqlServer(Configuration.GetConnectionString("connectionString"))
            );
            services.AddScoped<DataContext, DataContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection(); // force apis to respond about https

            app.UseRouting(); // ASP.NET MVC Routes

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
