using System.Linq;
using Kladara3.Data;
using Kladara3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kladara3.Controllers
{
    public class HomeController : Controller
    {
        private readonly Kladara3Context _context;

        public HomeController(Kladara3Context context)
        {
            _context = context;
        }

        // GET: Home
        public IActionResult Index()
        {
            var newTicketData = new NewTicketData
            {
                Wager = 0,
                Wallet = WalletTransactionsController.GetWalletState(_context),
                Bonus = 0,
                PossibleGain = 0.00
            };
            TicketsController.InitData(newTicketData);

            var vm = new
                MatchesNewTicketViewModel
            {
                Matches = _context.Match.ToList(),
                NewTicketData = newTicketData

            };

            return View(vm);
        }
    }
}
