using Datacom.TaxCalculator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Domain.Interfaces
{
    public interface IDataValidator
    {
        UserTax Validate(string[] input, int line);
    }
}
