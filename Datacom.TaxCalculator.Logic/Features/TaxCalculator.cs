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
                var rangeDifference = tableEntry.Upper - tableEntry.Lower;

                var sumdifference = annualSalary - sum;

                if (sumdifference > rangeDifference)
                {
                    sum += rangeDifference;
                    taxSum += rangeDifference * tableEntry.TaxRate;
                }
                else
                {
                    taxSum += sumdifference * tableEntry.TaxRate;
                    break;
                }   
            }

            var incomeTax = Math.Round(taxSum / 12,2);
            var netIncome = annualSalary - incomeTax;
            var grossIncome = Math.Round(annualSalary / 12,2);
            var superIncome = Math.Round(grossIncome * userTax.SuperRate,2);

            userTax.IncomeTax = incomeTax;
            userTax.NetIncome = netIncome;
            userTax.GrossIncome = grossIncome;
            userTax.SuperAmount = superIncome;
        }
    }
}
