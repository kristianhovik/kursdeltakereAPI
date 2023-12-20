using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using kursdeltakereAPI.Modeller;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace kursdeltakereAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DeltakerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));

            services.AddSingleton<IMongoClient>(_ =>
            {
                const string connectionUri = "mongodb+srv://hovik0029:<ruasoniD000229>@kurscluster.fcjhd8w.mongodb.net/?retryWrites=true&w=majority";
                var settings = MongoClientSettings.FromConnectionString(connectionUri);
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);
                return new MongoClient(settings);
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "kursdeltakereAPI", Version = "v1" });
            });

            // Legg til andre tjenester etter behov
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyMethod()
            .AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Legg til passende feilhåndteringsmiddel for produksjon
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Legg til andre middleware etter behov
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "kursdeltakere V1");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            // Konfigurer ruting
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
