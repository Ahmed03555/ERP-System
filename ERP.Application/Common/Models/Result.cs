using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }        
        public T? Value { get; set; }             
        public string? Error { get; set; }          
        public List<string>? Errors { get; set; }  

        public Result() { }

        private Result(bool isSuccess, T? value, string? error, List<string>? errors)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
            Errors = errors;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, value, null, null);
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T>(false, default, error, null);
        }

        public static Result<T> Failure(List<string> errors)
        {
            return new Result<T>(false, default, null, errors);
        }
    }
}
