using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int code, string msg = null)
        {
            StatusCode = code;
            ErrorMessage = msg ?? GetDefaultMessageForStatusCode(code);
        }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        private string GetDefaultMessageForStatusCode(int code)
        {
            return code switch
            {
                400 => "A bad request you have made",
                401 => "you are not authorized",
                404 => "No resource found",
                500 => "Server crashed!!! what the hell are you doing??",
                _ => "Hmm can not see what kind of error this is... go to hell"
            };
        }
    }
}