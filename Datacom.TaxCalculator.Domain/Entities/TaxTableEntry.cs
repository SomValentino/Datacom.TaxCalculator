using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Domain.Entities
{
    
    
    public class TaxTableEntry
    {
        public decimal? Upper { get; set; }
        public decimal? Lower { get; set; }
        public double TaxRate { get; set; }
    }
}
