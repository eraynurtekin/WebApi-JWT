using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilites.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data) : base(data, false)
        {
        }

        public ErrorDataResult(T data, string message) : base(data, false, message)
        {
        }
        public ErrorDataResult(string message) : base(default, false, message)
        {
            //Örnek 1
        }

        public ErrorDataResult() : base(default, false)
        {
            //Örnek 2
        }
    }
}
