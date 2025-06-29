namespace Elder.Core.Common.Results
{
    public readonly struct DomainResult
    {
        public readonly bool IsSuccess;
        public readonly int StatusCode;
        public readonly string Message;

        public DomainResult(bool isSuccess, int statusCode, string message = null)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Message = message;
        }

        public static DomainResult Success() => new DomainResult(true, 0); // 0을 성공 코드로 사용
        public static DomainResult Failure(int statusCode, string message = null) => new DomainResult(false, statusCode, message);
    }
}
