using Api.Common.MiddleException;
using Api.Installers;
using AutoMapper;
using Domain.Port;
using Dominio.Entidades;
using Dominio.Entidades.Identity;
using Infraestructura.EF;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServiceApplications.Mapper;
using ServiciosAplicacion;
using System.Text;
using Util.Common;
using Utilidades;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            this.Sql(services);
            services.AddResponseCompression();
            services.AddHttpContextAccessor();

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUtil, Utilities>();

            services.AddDependencyInjectionsInfrastructure();
            services.AddDependencyInjectionsApplications();
            this.JwtIdentity(services);

            this.ConfiguracionBase(services);
        }

        private void ConfiguracionBase(IServiceCollection services)
        {
            #region Mapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            services.AddControllers();

            #region swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = Constantes.NombreProyecto, Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "Bearer {token}",
                    In = ParameterLocation.Header,
                    Description = "Enter ‘Bearer’ [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                });
                c.OperationFilter<RequiredHeaderParameter>();
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                            {
                            new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new string[] {}
                            }
                    });
            });
            #endregion

            #region Cors
            services.AddCors(options =>
            {
                options.AddPolicy(name: Constantes.MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://example.com",
                                                          "http://localhost:4200", "*")
                                            .AllowAnyMethod()
                                            .AllowAnyHeader()
                                            .AllowCredentials();
                                  });
            });
            #endregion

        }

        private void Sql(IServiceCollection services)
        {
            services.AddDbContext<SqlContextEF>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("cloudjm-dev"), b => b.MigrationsAssembly("Api")));

            services.AddScoped<IMainContext>(provider => provider.GetService<SqlContextEF>());
        }

        private void JwtIdentity(IServiceCollection services)
        {
            #region JWT-identity-sqlserver
            services.AddIdentity<UsuarioAplicacion, IdentityRole>()
                .AddEntityFrameworkStores<SqlContextEF>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecretKey"]))
                };
            });
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseMiddleware<MiddleHandlerException>();

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", Constantes.DescripcionProyecto));

            app.UseRouting();

            app.UseCors(Constantes.MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
