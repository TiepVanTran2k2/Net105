using Application.Applications;
using Application.Contracts.Services;
using AutoMapper;
using Domain.Entities.Information;
using Domain.Repository;
using Domain.Services;
using EntityFrameworkCore.Entity;
using EntityFrameworkCore.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region DI
builder.Services.AddDbContext<DbContextApp>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddTransient<IInformationRepository, InformationRepository>();
builder.Services.AddTransient<IInformationService, InformationService>();
builder.Services.AddTransient<IHelperService, HelperService>();
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
