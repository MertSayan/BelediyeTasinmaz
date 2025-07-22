using Application.Features.MediatR.Users.Commands;
using Application.Interfaces;
using Application.Interfaces.PaymentInstallmentInterface;
using Application.Interfaces.PropertyInterface;
using Application.Interfaces.RentalInterface;
using Application.Interfaces.TokenInterface;
using Application.Interfaces.UserInterface;
using Application.MapperProfiles;
using AutoMapper;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Persistence.BackgroundServices;
using Persistence.Context;
using Persistence.Repositories;
using Persistence.Repositories.PaymentInstallmentRepository;
using Persistence.Repositories.PropertyRepository;
using Persistence.Repositories.RentalRepository;
using Persistence.Repositories.TokenRepository;
using Persistence.Repositories.UserRepository;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


            builder.Services.AddDbContext<HobiContext>(options =>
               options.UseSqlServer(connectionString));

            builder.Services.AddAuthentication("Bearer")
           .AddJwtBearer("Bearer", options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                   ValidAudience = builder.Configuration["JwtSettings:Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
               };
           });

            builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));

            builder.Services.Configure<DefaultUserSettings>(builder.Configuration.GetSection("DefaultUserSettings"));

            

            builder.Services.AddScoped<HobiContext>();
            builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            builder.Services.AddScoped<ITokenRepository, TokenRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPaymentInstallmentRepository, PaymentInstallmentRepository>();
            builder.Services.AddScoped<IRentalRepository, RentalRepository>();
            builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();


            builder.Services.AddHostedService<RentalStatusBackgroundService>(); //uygulama çalýþtýðý sürece servisi arka planda döndürecek.

            builder.Services.AddControllers();
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                // Diðer Swagger ayarlarýn varsa onlarýn altýna ekle:
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

                // Enum'larý string olarak göster
                c.UseInlineDefinitionsForEnums();
                c.SchemaGeneratorOptions = new SchemaGeneratorOptions
                {
                    SchemaIdSelector = type => type.FullName
                };

                //PropertyType dýþýnda baþka enum'lar için c.MapType<YourEnum>() satýrlarýný çoðaltabilirsin.
                c.MapType<PropertyType>(() => new OpenApiSchema
                {
                    Type = "string",
                    Enum = Enum.GetNames(typeof(PropertyType))
                               .Select(name => new OpenApiString(name))
                               .Cast<IOpenApiAny>()
                               .ToList()
                });
            });

            builder.Services.AddAutoMapper(typeof(PropertyProfile).Assembly);


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
