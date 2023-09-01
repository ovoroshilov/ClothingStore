using ClothingStore.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Domain.ViewModels.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "ClotheType")]
        [Required(ErrorMessage = "Choose type")]
        public ClotheType ClotheType { get; set; }

        [Display(Name="Imagen")]
        public IFormFile ImageFile { get; set; }
        public string? ImagePath { get; set; }
    }
}
