using Application.Applications;
using Application.Contracts.Services;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using AutoMapper;
using Azure.Storage.Blobs;
using Domain.Entities.ApplicationUser;
using Domain.Entities.Lab4;
using Domain.Entities.Product;
using Domain.Repository;
using Domain.Services;
using EntityFrameworkCore.Entity;
using EntityFrameworkCore.Repository;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("create", policy =>
    {
        policy.RequireRole("sm");
    });
});
#region DI
builder.Services.AddDbContext<DbContextApp>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddTransient<IHelperService, HelperService>();
builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<DbContextApp>();
builder.Services.AddTransient<IApplicationUserRepository, ApplicationRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ILab4Service, Lab4Service>();
builder.Services.AddTransient<ILab4Repository, Lab4Repository>();
#endregion
// Add Blob service client
builder.Services.AddSingleton(options => new BlobServiceClient(builder.Configuration.GetValue<string>("MangoConnection")));

builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });

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
app.UseNotyf();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
