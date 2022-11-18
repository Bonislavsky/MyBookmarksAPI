using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Domain.Helpers.ApiException.UserException
{
    public class VerifyPasswordUserException : Exception
    {
        public VerifyPasswordUserException(string message) : base(message)
        {

        }
    }
}
