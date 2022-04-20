namespace Common.Contract
{
    public class GlobalException : Exception
    {
        public readonly ResponseCode Code;

        public GlobalException(ResponseCode code) : base(ResponseMessage.ResponseMessages[code])
        {
            Code = code;
        }

        public GlobalException(ResponseCode code, string message) : base(message)
        {
            Code = code;
        }
    }
}
