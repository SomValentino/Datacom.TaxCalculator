using Datacom.TaxCalculator.Domain.Interfaces;
using Datacom.TaxCalculator.Infrastructure.Data;
using Datacom.TaxCalculator.Infrastructure.Validation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Infrastructure
{
    public static class ServiceInstaller
    {
        public static void AddTaxCalculatorInfrastructureInstaller(this IServiceCollection services)
        {
            services.AddScoped<IDataContext, CsvDataContext>();
            services.AddScoped<IDataValidator, CsvDataEntryValidator>();
        }
    }
}
