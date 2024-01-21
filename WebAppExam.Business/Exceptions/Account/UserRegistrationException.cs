using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppExam.Business.Exceptions.Account
{
    public class UserRegistrationException : Exception
    {
        public string ParamName { get; set; }
        public UserRegistrationException(string? message, string paramName) : base(message)
        {
            ParamName = paramName ?? string.Empty;
        }
    }
}
