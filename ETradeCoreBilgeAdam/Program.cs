using DataAccess.Contexts;
using DataAccess.Services;
using DataAccess.Services.Bases;
using ETradeCoreBilgeAdam.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

#region Localization
List<CultureInfo> cultures = new List<CultureInfo>()
{
    new CultureInfo("en-US")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name);
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});
#endregion

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // default 20 minutes
});

var section = builder.Configuration.GetSection(nameof(AppSettings));
section.Bind(new AppSettings());

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
    {
        config.LoginPath = "/Accounts/Home/Login";
        config.AccessDeniedPath = "/Accounts/Home/AccessDenied";
        config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        config.SlidingExpiration = true;
    });

#region IoC Container (Inversion of Control Container)
// Autofac, Ninject
var connectionString = builder.Configuration.GetConnectionString("ETicaretDb");
builder.Services.AddDbContext<Db>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<ProductServiceBase, ProductService>();
builder.Services.AddScoped<CategoryServiceBase, CategoryService>();
//builder.Services.AddSingleton<CategoryServiceBase, CategoryService>();
//builder.Services.AddTransient<CategoryServiceBase, CategoryService>();
builder.Services.AddScoped<ShopServiceBase, ShopService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<UserServiceBase, UserService>();
builder.Services.AddScoped<CountryServiceBase, CountryService>();
builder.Services.AddScoped<CityServiceBase, CityService>();
builder.Services.AddScoped<IReportService, ReportService>();
#endregion


var app = builder.Build();

#region Localization
app.UseRequestLocalization(new RequestLocalizationOptions()
{
    DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name),
    SupportedCultures = cultures,
    SupportedUICultures = cultures,
});
#endregion

AppCore8137.App.Environment.IsDevelopment = app.Environment.IsDevelopment();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
if (!AppCore8137.App.Environment.IsDevelopment)
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication(); 

app.UseAuthorization(); 

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "welcome",
      pattern: "Home/Index"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
