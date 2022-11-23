using System.Reflection;
using System.Text;
using BookStore.DBOperations;
using BookStore.Middlewares;
using BookStore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigurationManager configuration = builder.Configuration;

        // Add services to the container.
        builder.Services.AddDbContext<BookStoreDBContext>(Options => Options.UseInMemoryDatabase(databaseName: "BookStoreDB"));
        builder.Services.AddScoped<IBookStoreDBContext>(provider => provider.GetService<BookStoreDBContext>());
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Token:Issuer"],
                ValidAudience = configuration["Token:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"])),
                ClockSkew = TimeSpan.Zero
            };
        });

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            DataGenerator.Initialize(services);
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseCustomExceptionMiddleware();

        app.MapControllers();

        app.Run();
    }
}