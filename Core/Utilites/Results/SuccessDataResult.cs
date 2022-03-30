using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilites.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data) : base(data, success:true)
        {

        }

        public SuccessDataResult(T data, string message) : base(data, success: true, message)
        {

        }

        public SuccessDataResult(string message) : base(default, true, message)
        {
            //Örnek 1
        }

        public SuccessDataResult() : base(default, true)
        {
            //Örnek 2
        }
    }
}
