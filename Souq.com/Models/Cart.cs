using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Souq.com.Models
{
    public class Cart
    {
      
        public int Id { get; set; } 
        public int Qty { get; set; }    
        public float Price { get; set; }
        public string UserId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }  
        public Product Product { get; set; }

    }
}
