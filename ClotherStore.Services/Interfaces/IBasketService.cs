using ClothingStore.Domain.Response;
using ClothingStore.Domain.ViewModels.Product;

namespace ClotherStore.Services.Interfaces
{
    public interface IBasketService
    {
        Task<IBaseResponse<bool>> AddToBasket(ProductViewModel product);
    }
}
