using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Genie_Model.Model
{
    public class Expense
    {
        [Key]
        public Guid ExpenseID { get; set; } = Guid.NewGuid();
        [ForeignKey("User")]
        public Guid UserID { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public User User { get; set; }
    }

}
