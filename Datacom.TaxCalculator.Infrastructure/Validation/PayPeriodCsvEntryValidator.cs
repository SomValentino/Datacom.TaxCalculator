using Datacom.TaxCalculator.Domain.Entities;
using Datacom.TaxCalculator.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Infrastructure.Validation
{
    public class PayPeriodCsvEntryValidator : BaseCsvEntryValidator
    {
        public override void Validate(string[] values, int numline, UserTax userTax)
        {
            if (values.Length >= 5 && (string.IsNullOrWhiteSpace(values[4]) || string.IsNullOrEmpty(values[4])))
            {
                throw new CsvInvaildEntryException($"Pay period is required and cannot be empty at line: {numline}");
            }
            else if (!DateTime.TryParse($"{values[4]} 1, {DateTime.Now.Year}", out DateTime date))
            {
                throw new CsvInvaildEntryException($"Pay period has an invalid month value at line: {numline}");
            }
            else userTax.PayPeriod = values[4];
        }
    }
}
