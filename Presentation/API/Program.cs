using Application.Features.MediatR.Users.Commands;
using Application.Interfaces;
using Application.Interfaces.TokenInterface;
using Application.MapperProfiles;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence.Context;
using Persistence.Repositories;
using Persistence.Repositories.TokenRepository;
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




            builder.Services.AddControllers();
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
