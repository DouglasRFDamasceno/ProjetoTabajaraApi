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

var dbConnection = builder.Configuration["DbConnection"];

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

var symmetricSecurityKey = builder.Configuration["SymmetricSecurityKey"];

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

app.Run();
