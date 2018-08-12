using Kladara3.Models;
using Microsoft.EntityFrameworkCore;

namespace Kladara3.Data
{
    public class Kladara3Context : DbContext
    {
        public Kladara3Context (DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Match> Match { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Pair> Pair { get; set; }
        public DbSet<WalletTransaction> WalletTransaction { get; set; }
    }
}
