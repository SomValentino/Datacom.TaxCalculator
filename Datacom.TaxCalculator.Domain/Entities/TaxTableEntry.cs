using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Domain.Entities
{
    public class TaxTableEntry
    {
        public int Upper { get; set; }
        public int Lower { get; set; }
        public int TaxRate { get; set; }
    }
}
