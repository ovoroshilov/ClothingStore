using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Domain.Enums
{
    public enum ClotheType
    {
        [Display(Name = "Shirt")]
        Shirt = 0,
        [Display(Name = "T-Shirt")]
        TShirt = 1,
        [Display(Name = "Jeans")]
        Jeans = 2
    }
}
