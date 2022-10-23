using Datacom.TaxCalculator.Domain.Entities;
using Datacom.TaxCalculator.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Logic.Contracts
{
    public interface ITaxManager
    {
        Task<BatchProcessResult> BatchProcessAsync(string csvSourceFile);
    }
}
