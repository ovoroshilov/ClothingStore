namespace ClothingStore.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
