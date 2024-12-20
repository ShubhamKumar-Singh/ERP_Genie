using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Genie_Model.Model
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; } = Guid.NewGuid(); 
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public DateTime LastLogin { get; set; }
        public string? FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

    }
}