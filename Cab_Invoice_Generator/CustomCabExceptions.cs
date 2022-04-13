using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cab_Invoice_Generator
{
    public class CustomCabExceptions : Exception
    {
        public ExceptionType type;
        public enum ExceptionType
        {
            INVALID_DISTANCE,
            INVALID_TIME
        }
        public CustomCabExceptions(ExceptionType type, string message) : base(message)
        {
            this.type = type;
        }
    }
}
