using tpinmobiliariafinal.Models;
using tpinmobiliariafinal.Models.Objetos;
using tpinmobiliariafinal.Models.Repositorios;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;

namespace tpinmobiliariafinal
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.configuration = configuration;
            this.environment = environment;
            DataValues.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //el sitio web valida con cookie
                {
                    options.LoginPath = "/Usuario/Login";
                    options.LogoutPath = "/Usuario/Logout";
                    options.AccessDeniedPath = "/Home/Restringido";
                });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => //la api web valida con token
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["TokenAuthentication:Issuer"],
                        ValidAudience = configuration["TokenAuthentication:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(configuration["TokenAuthentication:SecretKey"])),
                    };
                });
            services.AddAuthorization(options =>
            {
                //options.AddPolicy("Empleado", policy => policy.RequireClaim(ClaimTypes.Role, "Administrador", "Empleado"));
                options.AddPolicy("Administrador", policy => policy.RequireRole("Administrador", "SuperAdministrador"));
            });
            /*
            Transient objects are always different; a new instance is provided to every controller and every service.
            Scoped objects are the same within a request, but different across different requests.
            Singleton objects are the same for every object and every request.
            */
            services.AddMvc();
            services.AddTransient<IRepositorio<Propietario>, RepositorioPropietario>();
            services.AddTransient<IRepositorioPropietario, RepositorioPropietario>();
            services.AddTransient<IRepositorio<Inquilino>, RepositorioInquilino>();
            services.AddTransient<IRepositorioInmueble, RepositorioInmueble>();

            services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Habilitar CORS
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // Uso de archivos estáticos (*.html, *.css, *.js, etc.)
            app.UseStaticFiles();
            app.UseRouting();

            // Habilitar autenticación
            app.UseAuthentication();
            app.UseAuthorization();

            // Permitir cookies
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.None,
            });

            // App en ambiente de desarrollo?
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); //página amarilla de errores
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("login", "login/{**accion}", new { controller = "Usuario", action = "Login" });
                endpoints.MapControllerRoute("rutaFija", "ruteo/{valor}", new { controller = "Home", action = "Ruta", valor = "defecto" });
                endpoints.MapControllerRoute("fechas", "{controller=Home}/{action=Fecha}/{anio}/{mes}/{dia}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
