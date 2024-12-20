using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Genie_Model.Model
{
    public class Transaction
    {
        [Key]
        public Guid TransactionID { get; set; } = Guid.NewGuid();
        [ForeignKey("User")]
        public Guid UserID { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; } // Credit/Debit
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public User User { get; set; }
    }

}
