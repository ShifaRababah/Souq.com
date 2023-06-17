namespace Souq.com.Models
{
    public class Order
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? UserId { get; set; }

        public List<OrderDetail>? OrderDetails { get; set; }
    }
}
