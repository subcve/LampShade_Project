namespace _0_Framework.Application
{
    public class OperationResult
    {
        public bool IsSucceed { get; set; }
        public string Message { get; set; }

        public OperationResult()
        {
            IsSucceed = false;
        }

        public OperationResult Succeed(string message = "عملیات با موفقیت انجام شد")
        {
            IsSucceed = true;
            Message = message;
            return this;
        }

        public OperationResult Failed(string message)
        {
            IsSucceed = false;
            Message = message;
            return this;
        }
    }
}
