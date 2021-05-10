using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Common
{
    public class AppSettings
    {
        private static readonly IConfigurationRoot config;
        static AppSettings()
        {
            config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
            connectionStrings = config.GetSection("ConnectionStrings").Get<ConnectionStrings>();           
        }    
        
        public static readonly ConnectionStrings connectionStrings;
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }
}
