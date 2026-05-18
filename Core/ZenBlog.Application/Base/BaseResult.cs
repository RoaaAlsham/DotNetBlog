
using System.Text.Json.Serialization;

namespace ZenBlog.Application.Base
{
    public class BaseResult<T>
    {
        public T? Data { get; set; }
        public IEnumerable<Error> Errors { get; set; } = [];

        [JsonIgnore]
        public bool IsSuccess => Errors == null || !Errors.Any();

        [JsonIgnore]
        public bool IsFailure => !IsSuccess;

        public static BaseResult<T> Success(T data)
        {
            return new BaseResult<T> { Data = data };
        }
        public static BaseResult<T> Failure(
            )
        {
            return new BaseResult<T> { Errors = [new Error { ErrorMessage="an unexpected error occurred"}] };
        }

        public static BaseResult<T> Failure(string errorMessage
      )
        {
            return new BaseResult<T> { Errors = [new Error { ErrorMessage = errorMessage }] };
        }
        public static BaseResult<T> Failure(IEnumerable<Error> errors)
        {
            return new BaseResult<T> { Errors = errors };
        }

        public static BaseResult<T> NotFound(string message)
        {
            return new BaseResult<T> { Errors = [new Error { ErrorMessage = message }] };
        }

    }

    public class Error
    {
        public string? PropertyName { get; set; }
        public string ErrorMessage { get; set; }

    }
}
