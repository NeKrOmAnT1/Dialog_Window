using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dialog_Window.Models
{
    internal class DatabaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

    }
}
