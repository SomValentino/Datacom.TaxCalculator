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
        public async Task TaxManager_LoadsCSV_ReturnsBatchProcessResult()
        {
            var taxManger = serviceProvider.GetService<ITaxManager>();

            var batchProcessResult = await taxManger.BatchProcessAsync("input.csv");

            Assert.Equal(batchProcessResult.NumberOfSuccessfulReads, 2);
            Assert.Equal(batchProcessResult.NumberOfFailedReads, 6);
            Assert.Equal(batchProcessResult.ErrorMessage, "Csv entry cannot be empty or whitespace at line: 1\n" +
                "Pay period has an invalid month value at line: 4\n" +
                "Cannot parse annual salary as an invalid value was passed at: 5\n" +
                "Super Rate must be between 0 and 50 percent inclusive at line : 6\n" +
                "FirstName is required and cannot be empty at line: 7\n" +
                "LastName is required and cannot be empty at line: 8");
        }
    }
}
