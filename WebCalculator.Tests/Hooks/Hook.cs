using System;
using BoDi;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;

namespace WebCalculator.Tests.Hooks
{
    [Binding]
    public class Hooks
    {   
        private const string _appSettingsFile = "appsettings.json";

        private readonly IObjectContainer _objectContainer;

        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void RegisterServices()
        {
            var factory = GetWebApplicationFactory();

            _objectContainer.RegisterInstanceAs(factory);
        }

        private static WebApplicationFactory<Program> GetWebApplicationFactory() =>
            new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureAppConfiguration((context, config) =>
                    {
                        // Hier kun je app settings overschrijven voor je testen
                        config.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), _appSettingsFile));
                    });

                    builder.ConfigureTestServices(services =>
                    {
                        // Hier kun je services registreren specfifiek voor je testen
                    });
                });
    }
}