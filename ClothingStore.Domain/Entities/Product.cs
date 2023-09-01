using ClothingStore.Domain.Enums;

namespace ClothingStore.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public ClotheType ClotheType { get; set; }
        public string? ImagePath { get; set; }
    }
}
