using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Writers;
using StoneDesafio.Business.Services;
using StoneDesafio.Entities;
using StoneDesafio.Models;

namespace StoneDesafio.Tests
{
    public class WebTestFixture : WebApplicationFactory<Program>
    {
        private IServiceScope scope;
        public IServiceProvider ServiceProvider { get; private set; }

        public WebTestFixture()
        {
            scope = Services.CreateScope();
            ServiceProvider = scope.ServiceProvider;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");
            builder.ConfigureServices(services =>
            {
                using(var scope = services.BuildServiceProvider().CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    db.Database.EnsureDeleted();
                    try
                    {
                        db.Database.EnsureCreated();
                    }
                    catch (Exception)
                    {

                    }
                }
            });
        }

        ~WebTestFixture()
        {
            scope.Dispose();
        }
    }
}
