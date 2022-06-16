using MediatR;
using products.Domain.Infra.Context;
using products.Domain.Infra.Repositories.ItemRepo;
using products.Domain.Itens.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddRepositories();
builder.Services.AddEntityFramework(builder.Configuration);
// builder.Services.AddCors(x => {
//     x.AddPolicy("default", p => {
//         p.WithOrigins("http://localhost:4200")
//         .AllowAnyHeader()
//         .AllowAnyMethod()
//         .AllowCredentials()
//         .SetIsOriginAllowed(x => true);
//     });
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// app.UseCors("default");

app.UseCors( x => 
{
    x.AllowAnyMethod();
    x.AllowAnyHeader();
    x.AllowAnyOrigin();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

