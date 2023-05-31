using Application.Applications;
using Application.Contracts.Services;
using AutoMapper;
using Domain.Entities.Product;
using Domain.Repository;
using Domain.Services;
using EntityFrameworkCore.Entity;
using EntityFrameworkCore.Repository;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region DI
builder.Services.AddDbContext<DbContextApp>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddTransient<IHelperService, HelperService>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<DbContextApp>();

builder.Services.AddTransient<IProductRepository, ProductRepository>();
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
