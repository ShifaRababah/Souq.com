using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Souq.com.Models
{
    public class Product
    {
        
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Image { get; set; }
        public string Description { get; set; } 
        public float Price { get; set; }
        public DateTime Date { get; set; }

        [NotMapped]
        [DisplayName("Upload Image")]   
        public IFormFile ImageFile { get; set; }


        [ForeignKey ("Category")]
        public int CategoryId { get; set; } 
        public Category Category { get; set; }

        public List<Cart> Carts { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }




    }



}
