using MediatR;
using MultiDB.API.Extensions;
using MultiDB.Application;
using MultiDB.Core.Repositories;
using MultiDB.Infrastructure.Data;
using MultiDB.Infrastructure.Repositories;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(Anchor).Assembly);

builder.Services.AddSingleton<IApplicationDbContextFactory, ApplicationDbContextFactory>();
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
builder.Services.AddScoped<DatabaseUpdater>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UpdateDatabase();

app.Run();
