using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Response;
using ClothingStore.Domain.ViewModels.Product;
using Microsoft.AspNetCore.Http;

namespace ClotherStore.Services.Interfaces
{
    public interface IProductService
    {
        Task<IBaseResponse<IEnumerable<Product>>> GetAllProducts();
        Task<IBaseResponse<ProductViewModel>> Create(ProductViewModel productViewModel);
        Task<IBaseResponse<Product>> Update(ProductViewModel productViewModel);
        Task<IBaseResponse<Product>> Delete(int id);
        Task<IBaseResponse<Product>> GetProduct(int id);
        Task<string> SaveImageAsync(IFormFile img);
    }
}
