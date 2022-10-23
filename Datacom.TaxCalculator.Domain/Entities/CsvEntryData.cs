using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Domain.Entities
{
    public class CsvEntryData
    {
        public CsvEntryData()
        {
            UserTaxes = new List<UserTax>();
            ErrorMessages = new List<string>();
        }
        public List<UserTax> UserTaxes { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
