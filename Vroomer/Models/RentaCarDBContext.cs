using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vroomer.Models
{
    public class RentaCarDBContext : DbContext
    {
        public RentaCarDBContext(DbContextOptions<RentaCarDBContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<RentaCar> RentedCars { get; set; }
    }
}
