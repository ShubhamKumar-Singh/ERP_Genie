using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Genie_Model.DTO
{
    public class SignupModel 
    { 
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; } 
        [Required][EmailAddress] public string Email { get; set; }
        [Required] public string FullName { get; set; }
        [Required] public DateTime DateOfBirth { get; set; } 
    }
}
