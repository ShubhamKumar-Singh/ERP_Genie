using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Genie_Model.Model
{
    public class Auth
    {
        [Key] public Guid AuthID { get; set; } = Guid.NewGuid(); 
        [Required] 
        public string Username { get; set; }
        [Required] public string PasswordHash { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}