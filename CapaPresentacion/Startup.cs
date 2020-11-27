using CapaDatos.Contextos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CapaDatos.Repositorios;
using CapaNegocio.InterfacesServicios;
using CapaNegocio.Servicios;
using CapaDatos.Cache;
using CapaDominio.RepositoryInterfaces;
using AutoMapper;

namespace CapaPresentacion
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
            services.AddControllersWithViews();

            //--- Aquí es donde deben inicializarse los contextos e informar cual sera su conexión a base de datos.

            //Lo correcto sería leer esta linea desde un archivo de configuración.
            //string connectionString = "Server=localhost;Database=cuadros;Trusted_Connection=True;";
            string connectionString = "Server=localhost;Database=cuadros;User Id=SA;Password=Password@12345";

            services.AddDbContext<GlobalContext>(options => options.UseSqlServer(connectionString));

            //Esta línea permite inyectar el respositorio de Colores en controladores, etc...
            services.AddScoped<IColoresRepository, ColoresRepository>();
            services.AddScoped<IColoresService, ColoresService>();
            services.AddScoped<IColoresCache, ColoresCache>();
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddDistributedMemoryCache();

            MapperConfiguration mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CapaNegocio.Mappers.AutoMapper());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            //TODO Cargar colores desde la cache
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GlobalContext globalContext)
        {
            //Reconstruye la base de datos si esta no existiese
            if (!env.IsProduction())
            {
                globalContext.Database.Migrate();
            }


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Estas llamadas de aquí nose 100% como funcionan.
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            // ----

            //Aquí se indica la primera página que llamará la aplicación al iniciarse.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Colores}/{action=Index}/{id?}");
            });
        }
    }
}
