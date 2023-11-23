using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moneyChanger.Exceptions
{
    public class UnavailableDataException : Exception
    {
        public UnavailableDataException(string? message) : base(message)
        {
        }
    }
}
