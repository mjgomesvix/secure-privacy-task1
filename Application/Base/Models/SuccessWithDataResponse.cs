namespace ApplicationLayer.Base.Models
{
    public class SuccessWithDataResponse<T> : SuccessResponse
    {
        internal SuccessWithDataResponse(T data, string message)
        {
            Data = data;
            base.Message = message;
        }
        public T Data { get; }
    }
}
