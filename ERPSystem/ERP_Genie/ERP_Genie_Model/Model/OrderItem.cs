using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Genie_Model.Model
{
    public class OrderItem
    {
        [Key]
        public Guid OrderItemID { get; set; } = Guid.NewGuid();
        [ForeignKey("Order")]
        public Guid OrderID { get; set; }
        [ForeignKey("Product")]
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }

}
