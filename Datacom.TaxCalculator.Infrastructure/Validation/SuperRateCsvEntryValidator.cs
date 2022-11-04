using Datacom.TaxCalculator.Domain.Entities;
using Datacom.TaxCalculator.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Infrastructure.Validation
{
    public class SuperRateCsvEntryValidator : BaseCsvEntryValidator
    {
        public override void Validate(string[] values, int numline, UserTax userTax)
        {
            var textvalue = values.Length >= 4 && values[3].EndsWith("%") ? values[3].Substring(0, values[3].Length - 1) : values[3];
            decimal superRate = 0.0M;
            if (!decimal.TryParse(textvalue, out superRate))
            {
                throw new CsvInvaildEntryException($"Cannot parse super rate as an invalid value was passed at: {numline}");
            }
            else if (superRate < 0.0M || superRate > 50.00M)
            {
                throw new CsvInvaildEntryException($"Super Rate must be between 0 and 50 percent inclusive at line : {numline}");
            }
            else userTax.SuperRate = superRate;
        }
    }
}
