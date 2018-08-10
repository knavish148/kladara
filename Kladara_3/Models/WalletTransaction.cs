using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kladara_3.Models
{
    public class WalletTransaction
    {
        public int Id { get; set; }
        public double WalletBefore { get; set; }
        public double WalletAfter { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
