namespace MoneyManager.Utilities
{
    public class DataResponse<T> : BaseResponse
    {
        public T? Data { get; set; }
    }
}