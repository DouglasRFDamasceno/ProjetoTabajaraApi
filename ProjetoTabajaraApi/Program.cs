using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjetoTabajaraApi.Data;
using ProjetoTabajaraApi.Models;
using ProjetoTabajaraApi.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DotNetEnv.Env.Load();
// string dbConnection = builder.Configuration.GetConnectionString("DB_CONNECTION");
string dbConnection = Environment.GetEnvironmentVariable("DB_CONNECTION");

System.Console.WriteLine(dbConnection);

// Add services to the container.
builder.Services.AddDbContext<appDbContext>(opts =>
{
    opts.UseLazyLoadingProxies().UseMySql(dbConnection, ServerVersion.AutoDetect(dbConnection));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<appDbContext>()
    .AddDefaultTokenProviders();

// string symmetricSecurityKey = builder.Configuration.GetConnectionString("SYMMETRIC_SECURITY_KEY");
string symmetricSecurityKey = Environment.GetEnvironmentVariable("SYMMETRIC_SECURITY_KEY");

System.Console.WriteLine(symmetricSecurityKey);
builder.Services.AddAuthentication(opts =>
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme
).AddJwtBearer(opts =>
{
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey
        (
        Encoding.UTF8.GetBytes(symmetricSecurityKey)
        ),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero,
    };
});

builder.Services.AddAuthorization();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<AddressService>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<appDbContext>();
    db.Database.Migrate();
}

app.Run();
