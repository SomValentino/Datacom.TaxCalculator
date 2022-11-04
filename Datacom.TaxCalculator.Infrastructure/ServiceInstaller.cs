using Datacom.TaxCalculator.Domain.Interfaces;
using Datacom.TaxCalculator.Infrastructure.Data;
using Datacom.TaxCalculator.Infrastructure.Manager;
using Datacom.TaxCalculator.Infrastructure.Validation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Infrastructure
{
    public static class ServiceInstaller
    {
        public static void AddTaxCalculatorInfrastructureInstaller(this IServiceCollection services)
        {
            services.AddScoped<IDataContext, CsvDataContext>();
            services.AddScoped<IValidatorManager, CsvValidatorManager>();
            services.AddScoped<IEnumerable<IDataValidator>>(options =>
            {
                var validators = new List<BaseCsvEntryValidator>();
                GetBaseCsvEntryValidators(validators);

                return validators;
            });
        }

        private static void GetBaseCsvEntryValidators(List<BaseCsvEntryValidator> validators)
        {
            var foundValidators = Assembly.GetAssembly(typeof(BaseCsvEntryValidator))?.GetTypes()
                                ?.Where(validator => validator.IsClass && 
                                !validator.IsAbstract && 
                                validator.IsSubclassOf(typeof(BaseCsvEntryValidator)));
            
            if (foundValidators != null && foundValidators.Any())
            {
                foreach (Type type in foundValidators)
                {
                    var typeValidator = (BaseCsvEntryValidator)Activator.CreateInstance(type);
                    if (typeValidator != null)
                        validators.Add(typeValidator);
                }
            }
            
        }
    }
}
