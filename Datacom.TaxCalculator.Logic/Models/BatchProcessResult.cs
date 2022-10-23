using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Logic.Models
{
    public class BatchProcessResult
    {
        public bool Success { get; set; }
        public int NumberOfSuccessfulReads { get; set; }
        public int NumberOfFailedReads { get; set; }
        public string ErrorMessage { get; set; }
    }
}
