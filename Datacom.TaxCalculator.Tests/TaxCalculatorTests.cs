using Datacom.TaxCalculator.Domain.Entities;
using Datacom.TaxCalculator.Logic.Contracts;
using Datacom.TaxCalculator.Tests.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Tests
{
    public class TaxCalculatorTests
    {
        private readonly ServiceProvider serviceProvider;
        private readonly IEnumerable<TaxTableEntry> taxTable;

        public TaxCalculatorTests()
        {
            serviceProvider = Startup.CreateServiceProvider();
            taxTable = serviceProvider.GetService<IEnumerable<TaxTableEntry>>();
        }

        [Fact]
        public void TaxCalculator_WhenAnnualSalaryIs600050_ReturnsPayRoll()
        {
            var taxCalculator = serviceProvider.GetService<ITaxCalculator>();

            var userTax = new UserTax
            {
                FirstName = "John",
                LastName = "Smith",
                AnnualSalary = 60050,
                SuperRate = 9,
                PayPeriod = "March"
            };

            taxCalculator.Calculate(taxTable, userTax);

            Assert.Equal(userTax.PayPeriod, $"01 March - 31 March");
            Assert.Equal(userTax.GrossIncome, 5004.17M);
            Assert.Equal(userTax.IncomeTax, 919.58M);
            Assert.Equal(userTax.NetIncome, 4084.59M);
            Assert.Equal(userTax.SuperAmount, 450.38M);

        }

        [Fact]
        public void TaxCalculator_WhenAnnualSalaryIs120000_ReturnsPayRoll()
        {
            var taxCalculator = serviceProvider.GetService<ITaxCalculator>();

            var userTax = new UserTax
            {
                FirstName = "Alex",
                LastName = "Wong",
                AnnualSalary = 120000,
                SuperRate = 10,
                PayPeriod = "March"
            };

            taxCalculator.Calculate(taxTable, userTax);

            Assert.Equal(userTax.PayPeriod, $"01 March - 31 March");
            Assert.Equal(userTax.GrossIncome, 10000.00M);
            Assert.Equal(userTax.IncomeTax, 2543.33M);
            Assert.Equal(userTax.NetIncome, 7456.67M);
            Assert.Equal(userTax.SuperAmount, 1000.00M);

        }

        [Fact]
        public void TaxCalculator_WhenAnnualSalaryIs250000_ReturnsPayRoll()
        {
            var taxCalculator = serviceProvider.GetService<ITaxCalculator>();

            var userTax = new UserTax
            {
                FirstName = "Test",
                LastName = "Test",
                AnnualSalary = 250000,
                SuperRate = 9,
                PayPeriod = "February"
            };

            taxCalculator.Calculate(taxTable, userTax);

            Assert.Equal(userTax.PayPeriod, $"01 February - 28 February");
            Assert.Equal(userTax.GrossIncome, 20833.33M);
            Assert.Equal(userTax.IncomeTax, 6468.33M);
            Assert.Equal(userTax.NetIncome, 14365.00M);
            Assert.Equal(userTax.SuperAmount, 1875.00M);

        }

        [Fact]
        public void TaxCalculator_WhenAnnualSalaryIs10000_ReturnsPayRoll()
        {
            var taxCalculator = serviceProvider.GetService<ITaxCalculator>();

            var userTax = new UserTax
            {
                FirstName = "Test",
                LastName = "Test",
                AnnualSalary = 10000,
                SuperRate = 9,
                PayPeriod = "February"
            };

            taxCalculator.Calculate(taxTable, userTax);

            Assert.Equal(userTax.PayPeriod, $"01 February - 28 February");
            Assert.Equal(userTax.GrossIncome, 833.33M);
            Assert.Equal(userTax.IncomeTax, 0.00M);
            Assert.Equal(userTax.NetIncome, 833.33M);
            Assert.Equal(userTax.SuperAmount, 75.00M);

        }

        [Fact]
        public void TaxCalculator_WhenAnnualSalaryIs10000AndSuperRate49_ReturnsPayRoll()
        {
            var taxCalculator = serviceProvider.GetService<ITaxCalculator>();

            var userTax = new UserTax
            {
                FirstName = "Test",
                LastName = "Test",
                AnnualSalary = 10000,
                SuperRate = 49,
                PayPeriod = "February"
            };

            taxCalculator.Calculate(taxTable, userTax);

            Assert.Equal(userTax.PayPeriod, $"01 February - 28 February");
            Assert.Equal(userTax.GrossIncome, 833.33M);
            Assert.Equal(userTax.IncomeTax, 0.00M);
            Assert.Equal(userTax.NetIncome, 833.33M);
            Assert.Equal(userTax.SuperAmount, 408.33M);

        }
    }
}
