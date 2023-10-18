using CiotNetNS.Domain.Enums;

namespace CiotNetNS.Shared
{
    public class Result
    {
        public ErrorCode Error { get; set; }

        public string Message { get; set; }

        public bool Ok => Error == ErrorCode.Ok;

        public bool Failed => Error != ErrorCode.Ok;

        public Result(ErrorCode errorCode, string message)
        {
            Error = errorCode;
            Message = message;
        }

        public static Result Success()
        {
            return new Result(ErrorCode.Ok, string.Empty);
        }

        public static Result Failure(ErrorCode errorCode = ErrorCode.Fail, string message = "")
        {
            return new Result(errorCode, message);
        }
    }
}
