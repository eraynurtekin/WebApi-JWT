using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilites.Results
{
    public class Result : IResult
    {
        public Result(bool success,string message) : this(success) //Diğer bir constructor ı tetikliyor
        {
            Message = message;
        }
        public Result(bool success)
        {
            Success = success;
        }
        public bool Success { get; }
        public string Message { get; }
    }
}
