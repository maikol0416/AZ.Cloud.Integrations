using Api.Common.MiddleException;
using Api.Installers;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServiceBus.HandlerAzureServiceBus;
using System.Text;
using Util.Common;
using Utilidades;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services);

var app = builder.Build();

app.UseMiddleware<MiddleHandlerException>();
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services)
{

    services.AddResponseCompression();
    services.AddHttpContextAccessor();

    //Estas inyecciones son requeridad para el QUEUE
    services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
    services.AddTransient<IUtil, Utilities>();
    builder.Services.AddTransient<IServicesBusHandler, ServiceSenderHandler>();

    services.AddAuthorization();
    services.AddControllers();


    ConfiguracionBase(services);
}

void ConfiguracionBase(IServiceCollection services)
{


    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();


    #region swagger
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "API TEST AZ CLOUD INTEGRATIONS", Version = "v1", Description = "" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "Bearer {token}",
            In = ParameterLocation.Header,
            Description = "Enter �Bearer� [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
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
        options.AddPolicy(name: Constants.MyAllowSpecificOrigins,
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

