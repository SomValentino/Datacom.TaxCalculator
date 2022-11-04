using Datacom.TaxCalculator.Domain.Entities;
using Datacom.TaxCalculator.Domain.Exceptions;
using Datacom.TaxCalculator.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Infrastructure.Manager
{
    public class CsvValidatorManager : IValidatorManager
    {
        private readonly IEnumerable<IDataValidator> _validators;

        public CsvValidatorManager(IEnumerable<IDataValidator> validators)
        {
            _validators = validators;
        }
        public UserTax Validate(string[] values, int numline)
        {
            var userTax = new UserTax();

            foreach (var validator in _validators)
            {
                validator.Validate(values, numline, userTax);

            }

            return userTax;
        }
    }
}
