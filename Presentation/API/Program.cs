using Application.Features.MediatR.Users.Commands;
using Application.Interfaces;
using Application.Interfaces.PaymentInstallmentInterface;
using Application.Interfaces.PropertyInterface;
using Application.Interfaces.RentalInterface;
using Application.Interfaces.TokenInterface;
using Application.Interfaces.UserInterface;
using Application.MapperProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence.BackgroundServices;
using Persistence.Context;
using Persistence.Repositories;
using Persistence.Repositories.PaymentInstallmentRepository;
using Persistence.Repositories.PropertyRepository;
using Persistence.Repositories.RentalRepository;
using Persistence.Repositories.TokenRepository;
using Persistence.Repositories.UserRepository;
using QuestPDF.Infrastructure;
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


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:3000") // React burada �al���yor
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));

            builder.Services.Configure<DefaultUserSettings>(builder.Configuration.GetSection("DefaultUserSettings"));

            QuestPDF.Settings.License = LicenseType.Community;


            builder.Services.AddScoped<HobiContext>();
            builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            builder.Services.AddScoped<ITokenRepository, TokenRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPaymentInstallmentRepository, PaymentInstallmentRepository>();
            builder.Services.AddScoped<IRentalRepository, RentalRepository>();
            builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
            builder.Services.AddScoped<IRentalStatisticRepository, RentalStatisticRepository>();
            builder.Services.AddScoped<IRentalReportService, RentalReportService>();

            builder.Services.AddHostedService<RentalStatusBackgroundService>(); //uygulama �al��t��� s�rece servisi arka planda d�nd�recek.
            builder.Services.AddHostedService<PenaltyCalculatorService>();

            builder.Services.AddControllers();
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                // Di�er Swagger ayarlar�n varsa onlar�n alt�na ekle:
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

                // Enum'lar� string olarak g�ster
                c.UseInlineDefinitionsForEnums();
                c.SchemaGeneratorOptions = new SchemaGeneratorOptions
                {
                    SchemaIdSelector = type => type.FullName
                };

                //PropertyType d���nda ba�ka enum'lar i�in c.MapType<YourEnum>() sat�rlar�n� �o�altabilirsin.
                //c.MapType<PropertyType>(()=> new OpenApiSchema
                //{
                //    Type = "string",
                //    Enum = Enum.GetNames(typeof(PropertyType))
                //               .Select(name => new OpenApiString(name))
                //               .Cast<IOpenApiAny>()
                //               .ToList()
                //});
                //c.MapType<PropertyStatus>(() => new OpenApiSchema
                //{
                //    Type="string",
                //    Enum=Enum.GetNames(typeof(PropertyStatus))
                //             .Select(name=>new OpenApiString(name))
                //             .Cast<IOpenApiAny>()
                //             .ToList()
                //});
            });

            builder.Services.AddAutoMapper(typeof(PropertyProfile).Assembly);


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowReactApp");


            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();


            app.MapControllers();

            app.Run();
        }
    }
}
