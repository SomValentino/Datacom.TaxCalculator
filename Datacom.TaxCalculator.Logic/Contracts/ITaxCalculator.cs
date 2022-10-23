using Datacom.TaxCalculator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Logic.Contracts
{
    public interface ITaxCalculator
    {
        void Calculate(IEnumerable<TaxTableEntry> taxTable, UserTax userTax);
    }
}
