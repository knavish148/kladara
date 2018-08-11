using System.Linq;
using System.Threading.Tasks;
using Kladara3.Data;
using Kladara3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kladara3.Controllers
{
    public class WalletTransactionsController : Controller
    {
        private readonly Kladara3Context _context;

        public WalletTransactionsController(Kladara3Context context)
        {
            _context = context;
        }

        // GET: WalletTransactions
        public async Task<IActionResult> Index()
        {
            return View(await _context.WalletTransaction.ToListAsync());
        }

        // GET: WalletTransactions/GetWalletState
        public static double GetWalletState(Kladara3Context context)
        {
            return context.WalletTransaction.Last().WalletAfter;
        }

        // GET: WalletTransactions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WalletTransactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WalletBefore,WalletAfter,TransactionDate")] WalletTransaction walletTransaction)
        {
            if (!ModelState.IsValid) return View(walletTransaction);

            _context.Add(walletTransaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: WalletTransactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walletTransaction = await _context.WalletTransaction
                .SingleOrDefaultAsync(m => m.Id == id);
            if (walletTransaction == null)
            {
                return NotFound();
            }

            return View(walletTransaction);
        }

        // POST: WalletTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var walletTransaction = await _context.WalletTransaction.SingleOrDefaultAsync(m => m.Id == id);
            _context.WalletTransaction.Remove(walletTransaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
