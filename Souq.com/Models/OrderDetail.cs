using System.ComponentModel.DataAnnotations.Schema;

namespace Souq.com.Models
{
    public class OrderDetail
    {
        public int Id { get; set; } 
        public float? Price { get; set; }    
        public int? Qty { get; set; }    
        public float? TotalPrice { get; set; }

        [ForeignKey("Order")]
        public int? OrderId { get; set; }
        public Order? Order { get; set; }

        [ForeignKey("Product")]
        public int? ProductId { get; set; }  
        public Product? Product { get; set; }    

    }
}
