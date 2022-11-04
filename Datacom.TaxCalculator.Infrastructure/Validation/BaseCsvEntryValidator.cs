using Datacom.TaxCalculator.Domain.Entities;
using Datacom.TaxCalculator.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Infrastructure.Validation
{
    public abstract class BaseCsvEntryValidator : IDataValidator
    {
        public abstract void Validate(string[] input, int line, UserTax userTax);
        
    }
}
