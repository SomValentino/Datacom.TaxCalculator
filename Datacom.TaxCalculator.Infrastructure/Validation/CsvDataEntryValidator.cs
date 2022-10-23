using Datacom.TaxCalculator.Domain.Entities;
using Datacom.TaxCalculator.Domain.Exceptions;
using Datacom.TaxCalculator.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Infrastructure.Validation
{
    public class CsvDataEntryValidator : IDataValidator
    {
        public UserTax Validate(string[] values, int numline)
        {
            var userTax = new UserTax();

            if (values.Length == 0 && string.IsNullOrWhiteSpace(values[0]) || string.IsNullOrEmpty(values[0]))
            {
                throw new CsvInvaildEntryException($"FirstName is required and cannot be empty at line: {numline}");
            }
            else userTax.FirstName = values[0];

            if (values.Length <= 1 && string.IsNullOrWhiteSpace(values[1]) || string.IsNullOrEmpty(values[1]))
            {
                throw new CsvInvaildEntryException($"LastName is required and cannot be empty at line: {numline}");
            }
            else userTax.LastName = values[1];

            if (values.Length <= 2 && !decimal.TryParse(values[2], out decimal annualSalary) && annualSalary <= 0.0M)
            {
                throw new CsvInvaildEntryException($"Cannot parse annualSalary as an invalid value was passed at: {numline}");
            }
            else userTax.AnnualSalary = decimal.Parse(values[2]);

            if (values.Length <= 3 && !decimal.TryParse(values[3], out decimal superRate) && (superRate < 0.0M || superRate > 50.00M))
            {
                throw new CsvInvaildEntryException($"Cannot parse super rate as an invalid value was passed at: {numline}");
            }
            else userTax.SuperRate = decimal.Parse(values[3]);

            if (values.Length <= 4 && string.IsNullOrWhiteSpace(values[4]) || string.IsNullOrEmpty(values[4]))
            {
                throw new CsvInvaildEntryException($"Pay period is required and cannot be empty at line: {numline}");
            }
            else userTax.PayPeriod = values[4];

            return userTax;
        }
    }
}
