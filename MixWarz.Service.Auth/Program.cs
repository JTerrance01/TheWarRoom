using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MixWarz.Service.Auth.Data;
using MixWarz.Service.Auth.Models;
using MixWarz.Service.Auth.Services;
using MixWarz.Service.Auth.Services.IService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

// Configure DB Context
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure JWT Options
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddControllers();


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        if (!app.Environment.IsDevelopment())
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth API");
            c.RoutePrefix = string.Empty;
        }
    }); ;
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

ApplyMigration();

app.Run();

void ApplyMigration()
{
    using(var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }        
    }
}
