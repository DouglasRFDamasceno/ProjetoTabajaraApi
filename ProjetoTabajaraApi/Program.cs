using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjetoTabajaraApi.Data;
using ProjetoTabajaraApi.Models;
using ProjetoTabajaraApi.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var userConnection = builder.Configuration["ConnectionStrings:UserConnection:"];
// Add services to the container.
builder.Services.AddDbContext<appDbContext>(opts =>
{
<<<<<<< HEAD
    opts.UseMySql(userConnection, ServerVersion.AutoDetect(userConnection));
=======
    opts.UseLazyLoadingProxies().UseMySql(userConnection, ServerVersion.AutoDetect(userConnection));
>>>>>>> develop
});

builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<appDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(opts =>
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme
).AddJwtBearer(opts =>
{
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey
        (
            Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"])
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
<<<<<<< HEAD
=======
builder.Services.AddScoped<AddressService>();
>>>>>>> develop

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
