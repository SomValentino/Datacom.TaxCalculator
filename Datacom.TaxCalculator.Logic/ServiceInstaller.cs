using Datacom.TaxCalculator.Domain.Entities;
using Datacom.TaxCalculator.Logic.Contracts;
using Datacom.TaxCalculator.Logic.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Logic
{
    public static class ServiceInstaller
    {
        public static void AddTaxCalculatorLogicInstaller(this IServiceCollection services)
        {
            services.AddScoped<ITaxCalculator, Features.TaxCalculator>();
            services.AddScoped<ITaxManager, TaxManager>();
        }
    }
}
