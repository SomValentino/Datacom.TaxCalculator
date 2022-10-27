using Datacom.TaxCalculator.Domain.Entities;
using Datacom.TaxCalculator.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Logic.Features
{
    public class TaxCalculator : ITaxCalculator
    {
        public void Calculate(IEnumerable<TaxTableEntry> taxTable, UserTax userTax)
        {
            var annualSalary = userTax.AnnualSalary;
            var sum = 0.0M;
            var taxSum = 0.0M;
            foreach(var tableEntry in taxTable)
            {
                var upper = tableEntry.Upper;
                var lower = tableEntry.Lower;

                var rangeDifference = upper.HasValue ? upper - lower : null;

                var sumdifference = annualSalary - sum;

                if (rangeDifference.HasValue && sumdifference > rangeDifference)
                {
                    sum += rangeDifference.Value;
                    taxSum += rangeDifference.Value * (decimal)(tableEntry.TaxRate/100);
                }
                else
                {
                    taxSum += sumdifference * (decimal)(tableEntry.TaxRate/100);
                    break;
                }   
            }

            var incomeTax = Math.Round(taxSum / 12,2);
            var grossIncome = Math.Round(annualSalary / 12,2);
            var netIncome = grossIncome - incomeTax;
            var superIncome = Math.Round(grossIncome * (userTax.SuperRate/100),2);

            userTax.IncomeTax = incomeTax;
            userTax.NetIncome = netIncome;
            userTax.GrossIncome = grossIncome;
            userTax.SuperAmount = superIncome;

            DateTime.TryParse($"{userTax.PayPeriod} 1, {DateTime.Now.Year}",out DateTime date);

            var firstDayofMonth = date.ToString("dd MMMMM");
            var lastDayofMonth = date.AddMonths(1).AddDays(-1).ToString("dd MMMMM");

            userTax.PayPeriod = $"{firstDayofMonth} - {lastDayofMonth}";
        }
    }
}
