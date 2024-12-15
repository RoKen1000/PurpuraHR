using Microsoft.EntityFrameworkCore;
using Purpura.DataAccess.DataContext;
using Microsoft.AspNetCore.Identity;
using Purpura.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using Purpura.MappingProfiles;
using Purpura.Repositories.Interfaces;
using Purpura.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(
    typeof(ApplicationUserMappingProfile), 
    typeof(AnnualLeaveMappingProfile)
);
builder.Services.AddDbContext<PurpuraDbContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<PurpuraDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IUserManagementRepository, UserManagementRepository>();
builder.Services.AddScoped<IAnnualLeaveRepository, AnnualLeaveRepository>();

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
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
