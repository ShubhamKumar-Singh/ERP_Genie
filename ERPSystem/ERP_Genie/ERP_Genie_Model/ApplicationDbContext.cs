using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_Genie_Model.Model;
using Microsoft.EntityFrameworkCore;

namespace ERP_Genie_Model
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Add your DbSets here
        public DbSet<User> users { get; set; }
    }
}
