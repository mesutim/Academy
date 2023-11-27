using Academy.Core.Convertors;
using Academy.Core.Services.Interfaces;
using Academy.Core.Services;
using Academy.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);

});

#endregion

#region Database Context
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion

#region IoC

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IViewRenderService, RenderViewToString>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IForumService, ForumService>();
#endregion

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();

#region AddCapcha

//builder.Services.AddDNTCaptcha(options =>
//    options.UseCookieStorageProvider()
//        .ShowThousandsSeparators(false)
//);



//IWebHostEnvironment _env = builder.Environment;

//builder.Services.AddDNTCaptcha(options =>
//{
//    // options.UseSessionStorageProvider() // -> It doesn't rely on the server or client's times. Also it's the safest one.
//    // options.UseMemoryCacheStorageProvider() // -> It relies on the server's times. It's safer than the CookieStorageProvider.
//    options.UseCookieStorageProvider(SameSiteMode.Strict /* If you are using CORS, set it to `None` */) // -> It relies on the server and client's times. It's ideal for scalability, because it doesn't save anything in the server's memory.
//                                                                                                        // .UseDistributedCacheStorageProvider() // --> It's ideal for scalability using `services.AddStackExchangeRedisCache()` for instance.
//                                                                                                        // .UseDistributedSerializationProvider()

//        // Don't set this line (remove it) to use the installed system's fonts (FontName = "Tahoma").
//        // Or if you want to use a custom font, make sure that font is present in the wwwroot/fonts folder and also use a good and complete font!
//        .UseCustomFont(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/fonts/ttf", "IRANSansWeb(FaNum)_Bold.ttf"))
//        .AbsoluteExpiration(minutes: 7)
//        .ShowThousandsSeparators(true)
//        .WithNoise( baseFrequencyX:250,  baseFrequencyY:300,20,30)
//        .WithEncryptionKey("This is my secure key!")        
//        .InputNames(// This is optional. Change it if you don't like the default names.
//            new DNTCaptchaComponent
//            {
//                CaptchaHiddenInputName = "DNT_CaptchaText",
//                CaptchaHiddenTokenName = "DNT_CaptchaToken",
//                CaptchaInputName = "DNT_CaptchaInputText"
//            })
//        .Identifier("dnt_Captcha")// This is optional. Change it if you don't like its default name.
//        .ShowExceptions=true;
//});



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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
