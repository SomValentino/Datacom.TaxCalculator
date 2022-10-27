using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Domain.Entities
{
    public class UserTax
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AnnualSalary { get; set; }
        public decimal SuperRate { get; set; }
        public string PayPeriod { get; set; }
        public decimal GrossIncome => Math.Round(AnnualSalary / 12,2);
        public decimal IncomeTax { get; set; }
        public decimal NetIncome => Math.Round(GrossIncome - IncomeTax,2);
        public decimal SuperAmount => Math.Round(GrossIncome * (SuperRate/100),2);
    }
}
