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
    public class MatchesController : Controller
    {
        private readonly Kladara_3Context _context;

        public MatchesController(Kladara_3Context context)
        {
            _context = context;
        }

        // GET: Matches
        public IActionResult Index()
        {
            NewTicketData newTicketData = new NewTicketData
            {
                Wager = 0,
                Wallet = WalletTransactionsController.GetWalletState(_context),
                PossibleGain = 0.00,
                Bonus = 0
            };

            MatchesNewTicketViewModel vm = new
                MatchesNewTicketViewModel
            {
                Matches = _context.Match.ToList(),
                NewTicketData = newTicketData

            };

            TicketsController.NewTicketData = newTicketData;

            return View(vm);
        }

        // ----------- HELPERS -----------

        // todo make this function async?
        public Match GetMatch(int matchId)
        {
            return _context.Match
                .SingleOrDefault(m => m.Id == matchId);
        }
    }
}
