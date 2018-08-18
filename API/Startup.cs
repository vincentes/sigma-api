using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using Pomelo.EntityFrameworkCore.MySql;
namespace API
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
            var mysql = true;
            if (!mysql)
            {
                string connectionString = "Server=tcp:nerdish.database.windows.net,1433;Initial Catalog=sigma;Persist Security Info=False;User ID=api;Password='WH;t@qK%+We}U5qV';MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                services.AddDbContext<SigmaContext>(x => x.UseSqlServer(connectionString));
            }
            else
            {
                MySqlConnectionStringBuilder connectionString = new MySqlConnectionStringBuilder()
                {
                    UserID = "api",
                    Server = "204.48.19.107",
                    Port = 3306,
                    Password = "DQej2zySKMwayVNf",
                    Database = "sigma"
                };
                services.AddDbContext<SigmaContext>(x => MySqlDbContextOptionsExtensions.UseMySql(x, connectionString.ToString()));
            }
            services.AddTransient<IRepository<Turno>, RTurno>();
            services.AddTransient<IRepository<Materia>, RMateria>();
            services.AddTransient<IRepository<Orientacion>, ROrientacion>();
            services.AddTransient<IRepository<Grupo>, RGrupo>();
            services.AddTransient<IUserRepository<Docente>, RDocente>();
            services.AddTransient<IRepository<GrupoDocente>, RGrupoDocente>();
            services.AddTransient<IRepository<TareaGrupo>, RTareaGrupo>();
            services.AddTransient<IRepository<Tarea>, RTarea>();
            services.AddTransient<IUserRepository<Alumno>, RAlumno>();
            services.AddTransient<IRepository<Imagen>, RImagen>();
            services.AddTransient<IRepository<Token>, RToken>();
            services.AddTransient<IRepository<Parcial>, RParcial>();
            services.AddTransient<IRepository<Escrito>, REscrito>();
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                options.User.RequireUniqueEmail = false;
            }).AddEntityFrameworkStores<SigmaContext>().AddDefaultTokenProviders();
            services.AddMvc();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SigmaContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            context.Database.EnsureCreated();
        }
    }
}
