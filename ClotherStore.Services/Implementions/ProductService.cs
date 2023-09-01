using ClotherStore.Services.Interfaces;
using ClothingStore.DAL.Interfaces;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Enums;
using ClothingStore.Domain.Response;
using ClothingStore.Domain.ViewModels.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClotherStore.Services.Implementions
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<Product> _productRepository;
        private ILogger<ProductService> _logger;

        public ProductService(IBaseRepository<Product> productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<IBaseResponse<ProductViewModel>> Create(ProductViewModel productViewModel)
        {
            var baseResponse = new BaseResponse<ProductViewModel>();
            try
            {
                var product = new Product
                {
                    Name = productViewModel.Name,
                    ClotheType = productViewModel.ClotheType,
                    Description = productViewModel.Description,
                    Price = productViewModel.Price,
                    ImagePath = productViewModel.ImagePath
                };
                await _productRepository.Create(product);

                baseResponse.StatusCode = StatusCode.Ok;
                baseResponse.Description = "Product was seccusfully added";
            }
            catch (Exception msg)
            {
                _logger.LogError(msg, $"[ProductService.Create]: {msg.Message}");
                return new BaseResponse<ProductViewModel>()
                {
                    Description = msg.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<Product>> Delete(int id)
        {
            var baseResponse = new BaseResponse<Product>();
            try
            {
                var product = await _productRepository.Get(id);
                if (product == null)
                {
                    return new BaseResponse<Product>
                    {
                        Description = "The object does not exist",
                        StatusCode = StatusCode.ProductNotFound
                    };
                }
                await _productRepository.Delete(product);

                baseResponse.StatusCode = StatusCode.Ok;
                baseResponse.Description = "Product was succesful deleted";
            }
            catch (Exception msg)
            {
                _logger.LogError(msg, $"[ProductService.Delete]: {msg.Message}");
                return new BaseResponse<Product>()
                {
                    Description = msg.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<Product>> GetProduct(int id)
        {
            var baseResponse = new BaseResponse<Product>();

            try
            {
                var product = await _productRepository.Get(id);
                if (product == null)
                {

                    baseResponse.Description = "Element not found";
                    baseResponse.StatusCode = StatusCode.ProductNotFound;
                    return baseResponse;
                }

                baseResponse.Data = product;
                baseResponse.StatusCode = StatusCode.Ok;
            }
            catch (Exception msg)
            {
                _logger.LogError(msg, $"[ProductService.GetProduct]: {msg.Message}");
                return new BaseResponse<Product>()
                {
                    Description = msg.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<IEnumerable<Product>>> GetAllProducts()
        {
            var baseResponse = new BaseResponse<IEnumerable<Product>>();

            try
            {
                var products = await _productRepository.GetAll().ToListAsync();

                if (products.Count == 0)
                {
                    baseResponse.Description = "There are zero elements";
                    baseResponse.StatusCode = StatusCode.Ok;
                    return baseResponse;
                }

                baseResponse.Data = products;
                baseResponse.StatusCode = StatusCode.Ok;
            }
            catch (Exception msg)
            {
                _logger.LogError(msg, $"[ProductService.GetAllProducts]: {msg.Message}");
                return new BaseResponse<IEnumerable<Product>>()
                {
                    Description = msg.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<Product>> Update(ProductViewModel productViewModel)
        {
            var baseResponse = new BaseResponse<Product>();
            try
            {
                var product = await _productRepository.Get(productViewModel.Id);
                if (product == null)
                {
                    baseResponse.StatusCode = StatusCode.ProductNotFound;
                    baseResponse.Description = "Product not found";
                    return baseResponse;
                }

                product.Name = productViewModel.Name;
                product.ClotheType = productViewModel.ClotheType;
                product.Description = productViewModel.Description;
                product.Price = productViewModel.Price;
                product.ImagePath = productViewModel.ImagePath;

                await _productRepository.Update(product);

                baseResponse.StatusCode = StatusCode.Ok;
                baseResponse.Description = "Product was seccusfully updated";
                return baseResponse;

            }
            catch (Exception msg)
            {
                _logger.LogError(msg, $"[ProductService.Update]: {msg.Message}");
                return new BaseResponse<Product>()
                {
                    Description = msg.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<string> SaveImageAsync(IFormFile img)
        {
            if (img == null || img.Length <= 0) return null;

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
            string filePath = "img/" + fileName; 

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await img.CopyToAsync(stream);
            }
            return filePath;
        }

    }
}
