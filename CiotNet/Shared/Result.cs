using CiotNetNS.Domain.Enums;

namespace CiotNetNS.Shared
{
    public class Result
    {
        public ErrorCode ErrorCode { get; set; }

        public string Message { get; set; }

        public bool OK => ErrorCode == ErrorCode.Success;

        public bool Error => ErrorCode != ErrorCode.Success;

        public Result(ErrorCode errorCode, string message)
        {
            ErrorCode = errorCode;
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
