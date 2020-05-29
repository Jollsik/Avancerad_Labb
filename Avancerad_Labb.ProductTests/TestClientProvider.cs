using Avancerad_Labb.Product.API;
using Avancerad_Labb.Product.API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Avancerad_Labb.ProductTests
{
    class TestClientProvider : IDisposable
    {
        public TestServer Server { get; private set; }
        public HttpClient Client { get; private set; }
        public TestClientProvider()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            WebHostBuilder webHostBuilder = new WebHostBuilder();
            webHostBuilder.ConfigureServices(s => s.AddDbContext<ProductDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"))));

            webHostBuilder.UseStartup<Startup>();

            Server = new TestServer(webHostBuilder);
            Client = Server.CreateClient();
        }
        public void Dispose()
        {
            Server?.Dispose();
            Client?.Dispose();
        }
    }
}
