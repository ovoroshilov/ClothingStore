using Microsoft.AspNetCore.Mvc;
using ClothingStore.Domain.ViewModels.Product;
using ClotherStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ClothingStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            var viewModel = new ProductViewModel(); 
            return View(viewModel);
        }

        [HttpPost]
      //  [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                if (productViewModel.ImageFile != null)
                {
                    string imgPath = await _productService.SaveImageAsync(productViewModel.ImageFile);
                    productViewModel.ImagePath = imgPath;
                }
                var response = await _productService.Create(productViewModel);
                if (response.StatusCode == Domain.Enums.StatusCode.Ok) return RedirectToAction("GetAllProducts");
            }
            return View(productViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await _productService.GetAllProducts();

            if (response.StatusCode == Domain.Enums.StatusCode.Ok) return View(response.Data);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(int id)
        {
            var response = await _productService.GetProduct(id);

            if (response.StatusCode == Domain.Enums.StatusCode.Ok) return View(response.Data);
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _productService.Delete(id);
            if (response.StatusCode == Domain.Enums.StatusCode.Ok) return RedirectToAction("CreateProduct");
            return RedirectToAction("Index");
        }
    }
}
