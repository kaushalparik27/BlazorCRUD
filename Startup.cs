using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using BlazorCRUD.Data;
using BlazorCRUD.Repository;

namespace BlazorCRUD {
    public class Startup {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddControllers();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddCors(options => {
                options.AddPolicy("AllowAll", builder => {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger) {
            logger.LogInformation("Configuring application middleware. Environment: {Environment}", env.EnvironmentName);

            if (env.IsDevelopment()) {
                logger.LogInformation("Running in Development mode. Developer exception page and Swagger enabled.");
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            } else {
                logger.LogInformation("Running in {Environment} mode.", env.EnvironmentName);
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            logger.LogInformation("Application middleware configured successfully.");
        }
    }
}