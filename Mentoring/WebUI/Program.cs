using Data.Concrete.EfCore;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using WebUI.Extension;
using WebUI.Hubs;

var builder = WebApplication.CreateBuilder(args);

    // IDentity Context bilgileri
    builder.Services.AddDbContext<Context>();
    builder.Services.AddIdentity<ProjeUser, ProjeRole>().AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();

    // SIGNALR TOPLAM 4 ADIM
    // SIGNALR ------------------------------------------------------
    builder.Services.AddSignalR();
    // SIGNALR CORS ------------------------------------------------------
    builder.Services.AddCors(options => options.AddDefaultPolicy( policy => policy
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed(i => true)));

    // IDENTITY AYARLARI - 1
    builder.Services.Configure<IdentityOptions>(options => {

        // Şifre Ayarları
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        // options.Password.RequiredLength = false;
        options.Password.RequireNonAlphanumeric = false;

        // Kilitleme Ayarları
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.AllowedForNewUsers = true;
        // Mail Ayarları
        // options.User.AllowedUserNameCharacters = ""; // UserName içinde olmasını istediğimiz harfler
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedEmail = true;
        options.SignIn.RequireConfirmedPhoneNumber = false;
    });

    // IDENTITY AYARLARI - 2
    builder.Services.ConfigureApplicationCookie(options => {
        options.LoginPath = "/User/UserLogin";
        options.LogoutPath = "/AdminAccount/Logout";
        options.AccessDeniedPath = "/User/UserLogin";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.Cookie = new CookieBuilder
        {
            HttpOnly = true,
            Name = ".medikal.Security.Cookie",
            SameSite = SameSiteMode.Strict
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

    // 404 hatası için yazdık. Error Controller içindeki Error404 Actionuna, code parametresi ile gidecek
    app.UseStatusCodePagesWithReExecute("/Error/Error404", "?code={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors(); // SIGNALR CORS ÇAĞIRMAK İÇİN
app.UseRouting();

    app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/chathub");
app.MigrateDatabase().Run();