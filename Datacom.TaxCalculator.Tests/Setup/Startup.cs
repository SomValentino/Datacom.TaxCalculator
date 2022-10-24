using Datacom.TaxCalculator.Domain.Entities;
using Datacom.TaxCalculator.Infrastructure;
using Datacom.TaxCalculator.Logic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datacom.TaxCalculator.Infrastructure.Data;
using Microsoft.Extensions.Logging.Abstractions;
using Datacom.TaxCalculator.Logic.Features;

namespace Datacom.TaxCalculator.Tests.Setup
{
    public static class Startup
    {
        public static ServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();

            var taxconfiguration = new Dictionary<string, string>
            {
                {"TaxTableEntry", "[{\"upper\": 14000,\"lower\":0,\"taxRate\":10.5}," +
                "{\"upper\": 48000,\"lower\":14000,\"taxRate\":17.5}, " +
                "{\"upper\": 70000,\"lower\":48000,\"taxRate\":30}," +
                "{\"upper\": 180000,\"lower\":70000,\"taxRate\":33}," +
                "{\"upper\": null,\"lower\":180000,\"taxRate\":39}]" }
            };

            var configuration = new ConfigurationBuilder()
                                .AddInMemoryCollection(taxconfiguration)
                                .Build();

            services.AddSingleton<IConfiguration>(options =>
            {
                return configuration;
            });


            services.AddSingleton<IEnumerable<TaxTableEntry>>(options =>
            {

                return JsonConvert.DeserializeObject<List<TaxTableEntry>>(configuration["TaxTableEntry"]);


            });
            services.AddTaxCalculatorInfrastructureInstaller();
            services.AddTaxCalculatorLogicInstaller();
            services.AddScoped<ILogger<CsvDataContext>, NullLogger<CsvDataContext>>();
            services.AddScoped<ILogger<TaxManager>, NullLogger<TaxManager>>();

            var svcProvider = services.BuildServiceProvider();

            return svcProvider;
        }
    }
}
