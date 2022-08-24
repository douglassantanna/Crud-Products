using System.Text;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using products.Domain.Helpers;
using products.Domain.Infra.Context;
using products.Domain.Omie;
using products.Domain.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AutoMapper(builder.Configuration);
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton((Func<IServiceProvider, OmieConfigurations>)(x =>
{
    string omie_url = builder.Configuration.GetSection("OmieSettings:OMIE_URL").Value;
    string app_key = builder.Configuration.GetSection("OmieSettings:OMIE_APP_KEY").Value;
    string app_secret = builder.Configuration.GetSection("OmieSettings:OMIE_APP_SECRET").Value;
    return new(
                omie_url,
                app_key,
                app_secret);
}));
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddRepositories();
builder.Services.AddEntityFramework(builder.Configuration);
builder.Services.AddValidators();

var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("Auth:Secret").Value);
builder.Services.AddAuthentication(
    a =>
    {
        a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
).AddJwtBearer(
    j =>
    {
        j.RequireHttpsMetadata = false;
        j.SaveToken = true;
        j.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x =>
{
    x.AllowAnyMethod();
    x.AllowAnyHeader();
    x.AllowAnyOrigin();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

