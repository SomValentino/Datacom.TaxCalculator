using Datacom.TaxCalculator.Domain.Entities;
using Datacom.TaxCalculator.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Infrastructure.Validation
{
    public class CsvNameValidator : BaseCsvEntryValidator
    {
        public override void Validate(string[] values, int numline, UserTax userTax)
        {

            if (values.Length >= 1 && string.IsNullOrWhiteSpace(values[0]) || string.IsNullOrEmpty(values[0]))
            {
                throw new CsvInvaildEntryException($"FirstName is required and cannot be empty at line: {numline}");
            }
            else userTax.FirstName = values[0];

            if (values.Length >= 2 && string.IsNullOrWhiteSpace(values[1]) || string.IsNullOrEmpty(values[1]))
            {
                throw new CsvInvaildEntryException($"LastName is required and cannot be empty at line: {numline}");
            }
            else userTax.LastName = values[1];
        }
    }
}
