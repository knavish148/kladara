using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Kladara_3.Models;

namespace Kladara_3.Models
{
    public class Kladara_3Context : DbContext
    {
        public Kladara_3Context (DbContextOptions<Kladara_3Context> options)
            : base(options)
        {
        }

        public DbSet<Kladara_3.Models.Match> Match { get; set; }
        public DbSet<Kladara_3.Models.Ticket> Ticket { get; set; }
        public DbSet<Kladara_3.Models.Pair> Pair { get; set; }
        public DbSet<Kladara_3.Models.WalletTransaction> WalletTransaction { get; set; }
    }
}
