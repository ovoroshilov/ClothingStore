using ClothingStore.Domain.Enums;

namespace ClothingStore.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public StatusCode StatusCode { get; set; }
        public string Description { get; set; }
        public T Data { get; set; }
    }

    public interface IBaseResponse<T>
    {
        StatusCode StatusCode { get; }
        string Description { get; }
        T Data { get; }
    }
}
