using Datacom.TaxCalculator.Domain.Entities;
using Datacom.TaxCalculator.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Infrastructure.Validation
{
    public class AnnualSalaryCsvEntryValidator : BaseCsvEntryValidator
    {
        public override void Validate(string[] values, int numline, UserTax userTax)
        {
            decimal result = 0.0M;
            if (values.Length >= 3 && !decimal.TryParse(values[2], out result))
            {

                throw new CsvInvaildEntryException($"Cannot parse annual salary as an invalid value was passed at: {numline}");

            }
            else if (result <= 0.0M)
            {
                throw new CsvInvaildEntryException($"Cannot parse annual salary as an invalid value was passed at: {numline}");
            }
            else userTax.AnnualSalary = result;
        }
    }
}
