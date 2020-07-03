using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MantenimientoGrupoGB.BL;
using MantenimientoGrupoGB.BL.Interfaces;
using MantenimientoGrupoGB.DAL;
using MantenimientoGrupoGB.DAL.Context;
using MantenimientoGrupoGB.DAL.Interfaces;
using MantenimientoGrupoGB.EN.ConfiguracionGeneral;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NToastNotify;
using Newtonsoft.Json.Converters;

namespace MantenimientoGrupoGB.WEB
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

            //Configurando acceso al appsettings
            var appSettingsSection = Configuration.GetSection("Config");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();

            //ConexionBD
            services.AddDbContext<MantenimientogrupogbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));

            //Inyeccion de servicios DAL

            services.AddScoped<IUsuarioBaseDAL, UsuarioBaseDAL>();

            //Inyeccion de servicios BL
            services.AddScoped<IUsuarioBaseBL, UsuarioBaseBL>();

            services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
            {
                ProgressBar = false,
                PositionClass = ToastPositions.TopRight
            });
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            }).AddRazorRuntimeCompilation();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseNToastNotify();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=AdministrarUsuario}/{action=Index}/{id?}");
            });
        }
    }
}
