using Datacom.TaxCalculator.Domain.Entities;
using Datacom.TaxCalculator.Domain.Exceptions;
using Datacom.TaxCalculator.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Infrastructure.Validation
{
    public class CsvDataEntryValidator : IDataValidator
    {
        private readonly decimal _maxSuperRate;

        public CsvDataEntryValidator(IConfiguration configuration)
        {
            _maxSuperRate = decimal.Parse(configuration["MaxSuperRate"]);
        }
        public UserTax Validate(string[] values, int numline)
        {
            var userTax = new UserTax();

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

            var textvalue = values.Length >= 4 && values[3].EndsWith("%") ? values[3].Substring(0, values[3].Length - 1) : values[3];
            decimal superRate = 0.0M;
            if (!decimal.TryParse(textvalue, out superRate))
            {
                throw new CsvInvaildEntryException($"Cannot parse super rate as an invalid value was passed at: {numline}");
            }
            else if (superRate < 0.0M || superRate > _maxSuperRate)
            {
                throw new CsvInvaildEntryException($"Super Rate must be between 0 and 50 percent inclusive at line : {numline}");
            }
            else userTax.SuperRate = superRate;

            if (values.Length >= 5 && (string.IsNullOrWhiteSpace(values[4]) || string.IsNullOrEmpty(values[4])))
            {
                throw new CsvInvaildEntryException($"Pay period is required and cannot be empty at line: {numline}");
            }
            else if (!DateTime.TryParse($"{values[4]} 1, {DateTime.Now.Year}",out DateTime date))
            {
                throw new CsvInvaildEntryException($"Pay period has an invalid month value at line: {numline}");
            }
            else userTax.PayPeriod = values[4];

            return userTax;
        }
    }
}
