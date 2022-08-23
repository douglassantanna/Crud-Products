using System;
using MediatR;
using products.Domain.Infra.Context;
using products.Domain.Omie;
using products.Domain.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
// {
//     OmieConfigurations config = new OmieConfigurations();
//     config.OMIE_URL = Environment.GetEnvironmentVariable("OmieSettings:OMIE_URL");
//     config.APP_KEY = Environment.GetEnvironmentVariable("OmieSettings:OMIE_APP_KEY");
//     config.APP_SECRET = Environment.GetEnvironmentVariable("OmieSettings:OMIE_APP_SECRET");
//     return config;
// });
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddRepositories();
builder.Services.AddEntityFramework(builder.Configuration);
builder.Services.AddValidators();

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

