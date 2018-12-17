using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CloudManager.Models
{
    public class Customer
    {
        [Key]
        [DisplayName("Customer ID")]
        public int CustomerID { get; set; }
        
        [Required]
        [DisplayName("Customer name")]
        public string Name { get; set; }
    }
}
