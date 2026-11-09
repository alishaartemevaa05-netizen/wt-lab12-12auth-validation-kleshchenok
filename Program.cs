using System.Globalization;
using Microsoft.EntityFrameworkCore;
using wt_lab12_12auth_validation_kleshchenok.Data;
using wt_lab12_12auth_validation_kleshchenok.Helpers;

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

var builder = WebApplication.CreateBuilder(args);

// ───────────── Подключение к PostgreSQL ─────────────
var connectionString = ConnectionStringResolver.Resolve(builder.Configuration);
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

// Регистрация Razor Pages
builder.Services.AddRazorPages();

// ───────────── Session (WT-12) ─────────────
// Session хранит данные пользователя на сервере; браузеру выдаётся только cookie с идентификатором сессии.
// Для хранения данных сессии нужен кэш: используем кэш в памяти приложения.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);   // через 30 минут бездействия сессия «протухает»
    options.Cookie.HttpOnly = true;                   // cookie недоступна из JavaScript
    options.Cookie.IsEssential = true;                // cookie нужна для работы входа
});

var app = builder.Build();

// ───────────── Автоматическое применение миграций ─────────────
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    app.Logger.LogInformation("Проверка и применение миграций PostgreSQL...");
    db.Database.Migrate();
    app.Logger.LogInformation("Миграции применены. База данных готова.");
}

// Стандартные middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();          // WT-12: после UseRouting и до MapRazorPages

app.UseAuthorization();
app.MapRazorPages();

// ───────────── Порт для Render ─────────────
var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Run($"http://0.0.0.0:{port}");
