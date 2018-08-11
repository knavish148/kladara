using System;

namespace Kladara3.Models
{
    public class WalletTransaction
    {
        public int Id { get; set; }
        public double WalletBefore { get; set; }
        public double WalletAfter { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
