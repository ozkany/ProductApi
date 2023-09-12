using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Domain.Exceptions
{
    public class CustomException : ApplicationException
    {
        public CustomException(string message) : base(message) { }

        public int ErrorCode { get; set; }
    }
}
