using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using app.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("KontekstBazeConnection") ?? throw new InvalidOperationException("Connection string 'KontekstBazeConnection' not found.");
builder.Services.AddDbContext<KontekstBaze>(options => options.UseSqlite());

builder.Services.AddIdentity<Vlagatelj, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
}).AddEntityFrameworkStores<KontekstBaze>().AddDefaultUI().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.LoginPath = "/Pages/Account/Login";
    options.LogoutPath = "/Pages/Account/Logout";
    options.AccessDeniedPath = "/Pages/Account/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<KontekstBaze>();

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<Vlagatelj>>();

    string[] roleNames = { "Admin", "User" };
    foreach (var roleName in roleNames)
    {
        var roleExists = await roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    var email = "admin@mz.si";
    var password = "Admin123!";

    var user = new Vlagatelj
    {
        Ime = "Ministrstvo za zdravje",
        Priimek = "",
        ImeZavoda = "Ministrstvo za zdravje",
        Email = email,
        UserName = email
    };

    var userExists = await userManager.FindByEmailAsync(email);
    if (userExists == null)
    {
        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
