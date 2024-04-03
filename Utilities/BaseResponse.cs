namespace MoneyManager.Utilities
{
    public class BaseResponse
    {
        public int Code { get; set; } = StatusCodes.Status200OK;
        public string Message { get; set; } = "Successful.";
    }
}