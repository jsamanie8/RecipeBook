using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recipe.Services_V2.Interfaces;
using Recipe.Services_V2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipe.Web.Api.StartUpConfig
{
    public class DependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            if (config is IConfiguration)
            {
                services.AddSingleton<IConfiguration>(config as IConfigurationRoot);
            }

            services.AddSingleton<IConfiguration>(config);
            string connectionString = config.GetConnectionString("Default");


            services.AddSingleton<IOwnerService, OwnerService>();
        }
    }
}
