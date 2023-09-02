using ClothingStore.Domain.Enums;

namespace ClothingStore.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }    
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
