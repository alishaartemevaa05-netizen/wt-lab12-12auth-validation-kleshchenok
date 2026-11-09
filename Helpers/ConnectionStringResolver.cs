namespace wt_lab12_12auth_validation_kleshchenok.Helpers;

public static class ConnectionStringResolver
{
    public static string Resolve(IConfiguration configuration)
    {
        var databaseUrl = configuration["postgresql://wt_lab8_db_kleshchenok_user:GK4GBOgPASzqB1rPlqliGTz8FFZUosoR@dpg-dbl3enbncjis73fo2a10-a.oregon-postgres.render.com/wt_lab8_db_kleshchenok"];
        if (!string.IsNullOrWhiteSpace(databaseUrl))
        {
            var uri = new Uri(databaseUrl);
            var userInfo = uri.UserInfo.Split(':', 2);
            var port = uri.Port > 0 ? uri.Port : 5432;

            var external = uri.Host.EndsWith(".render.com", StringComparison.OrdinalIgnoreCase);
            var ssl = external ? "SSL Mode=Require;Trust Server Certificate=true" : "SSL Mode=Disable";

            // Вот правильная склейка строки (все части внутри одной команды return)
            return $"Host={uri.Host};Port={port};Database={uri.AbsolutePath.TrimStart('/')};" +
                   $"Username={Uri.UnescapeDataString(userInfo[0])};" +
                   $"Password={Uri.UnescapeDataString(userInfo.Length > 1 ? userInfo[1] : "")};" +
                   "Gss Encryption Mode=Disable;" + ssl;
        }

        return configuration.GetConnectionString("DefaultConnection")
               ?? throw new InvalidOperationException("Не задана строка подключения (DATABASE_URL или DefaultConnection).");
    }
}