using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiException : ApiResponse
    {
        public ApiException(int statusCode, string msg = null, string details = null) : base(statusCode, msg)
        {
            this.Details = details;
        }
        public string Details { get; set; }
    }
}