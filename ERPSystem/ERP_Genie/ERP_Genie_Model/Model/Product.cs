using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Genie_Model.Model
{
    public class Product
    {
        [Key]
        public Guid ProductID { get; set; } = Guid.NewGuid();
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int StockLevel { get; set; }
    }

}
