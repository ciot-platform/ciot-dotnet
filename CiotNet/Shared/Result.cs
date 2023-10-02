using CiotNetNS.Domain.Enums;

namespace CiotNetNS.Shared
{
    public class Result
    {
        public ErrorCode Error { get; set; }

        public string Message { get; set; }

        public bool Ok => Error == ErrorCode.Success;

        public bool Failed => Error != ErrorCode.Success;

        public Result(ErrorCode errorCode, string message)
        {
            Error = errorCode;
            Message = message;
        }

        public static Result Success()
        {
            return new Result(ErrorCode.Success, string.Empty);
        }

        public static Result Failure(ErrorCode errorCode = ErrorCode.Fail, string message = "")
        {
            return new Result(errorCode, message);
        }
    }
}
