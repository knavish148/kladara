using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kladara_3.Models;

namespace Kladara_3.Controllers
{
    public class WalletTransactionsController : Controller
    {
        private readonly Kladara_3Context _context;

        public WalletTransactionsController(Kladara_3Context context)
        {
            _context = context;
        }

        // GET: WalletTransactions
        public async Task<IActionResult> Index()
        {
            return View(await _context.WalletTransaction.ToListAsync());
        }

        // GET: WalletTransactions/GetWalletState
        public static double GetWalletState(Kladara_3Context context)
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
            if (ModelState.IsValid)
            {
                _context.Add(walletTransaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(walletTransaction);
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

        private bool WalletTransactionExists(int id)
        {
            return _context.WalletTransaction.Any(e => e.Id == id);
        }
    }
}
