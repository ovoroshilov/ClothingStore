using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
