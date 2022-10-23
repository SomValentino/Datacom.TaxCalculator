using Datacom.TaxCalculator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Domain.Interfaces
{
    public interface IDataContext
    {
        Task<CsvEntryData> Read(string source);
        Task Write(IEnumerable<UserTax> data, string destination);
    }
}
