using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    public class InternalServerException : Exception
    {
        public InternalServerException(string msg) :base(msg) {}
        public InternalServerException(string msg,string details) :base(msg)
        {
           Details=details;
        }
        public string? Details { get;  }

    }
}
