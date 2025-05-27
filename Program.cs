using keylogger_lib.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server_Library.Data;
using Server_Library.Repositories.Implementation;
using serverLibrary.Helper;
using serverLibrary.Respositories.contract;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JwtSection>(builder.Configuration.GetSection("JwtSection"));
var jwtSection = builder.Configuration.GetSection(nameof(JwtSection)).Get<JwtSection>();
//services added
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Sorry Connection String not found"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtSection!.Issuer,
        ValidAudience = jwtSection.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.Key))

    };
});
builder.Services.Configure<JwtSection>(builder.Configuration.GetSection("JwtSection"));

//Repository Dependencies
builder.Services.AddScoped<IuserAccount, UserAccountRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<KeyloggerLog>, KeyloggerInfoRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedBlazorWasm",
    builder => builder
    .WithOrigins("https://localhost:7185", "http://localhost:5011")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("AllowedBlazorWasm");

app.UseAuthorization();
app.UseAuthorization();
app.MapControllers();

app.Run();
