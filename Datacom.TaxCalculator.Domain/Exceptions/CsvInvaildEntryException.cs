using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.TaxCalculator.Domain.Exceptions
{
    public class CsvInvaildEntryException : Exception
    {
        public CsvInvaildEntryException()
        {

        }
        public CsvInvaildEntryException(string message): base(message)
        {

        }

        public CsvInvaildEntryException(Exception exception, string message): base(message, exception)
        {

        }
    }
}
