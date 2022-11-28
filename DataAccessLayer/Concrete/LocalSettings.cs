using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Concrete;

public static class LocalSettings
{
    public static string GetConnectionString(string jsonFileName)
    {
        var jsonFile = new ConfigurationBuilder().AddJsonFile($"{jsonFileName}").Build();
        var connectionString = jsonFile.GetSection("ConnectionStrings")["defaultConnection"];
        return connectionString ?? throw new KeyNotFoundException("Connection string was not found.");
    }
}