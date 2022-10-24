using Datacom.TaxCalculator.Domain.Entities;
using Datacom.TaxCalculator.Domain.Interfaces;
using Datacom.TaxCalculator.Logic.Contracts;
using Datacom.TaxCalculator.Logic.Features;
using Datacom.TaxCalculator.Tests.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Tests
{
    public class TaxManagerTests
    {
        private readonly ServiceProvider serviceProvider;

        public TaxManagerTests()
        {
            serviceProvider = Startup.CreateServiceProvider();
           
        }

        [Fact]
        public void TaxManager_LoadsCSV_ReturnsBatchProcessResult()
        {
            var taxManger = serviceProvider.GetService<ITaxManager>();

            var batchProcessResult = taxManger.BatchProcessAsync("input.csv");
        }
    }
}
