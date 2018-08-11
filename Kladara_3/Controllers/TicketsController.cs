using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kladara3.Data;
using Kladara3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kladara3.Controllers
{
    public class TicketsController : Controller
    {
        private readonly Kladara3Context _context;
        private static List<Pair> _userPairs = new List<Pair>();

        // ctor
        public TicketsController(Kladara3Context context)
        {
            _context = context;
        }

        // static field getter/setter
        public static NewTicketData NewTicketData { get; set; }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            NewTicketData = new NewTicketData
            {
                Wager = 0,
                Wallet = WalletTransactionsController.GetWalletState(_context),
                PossibleGain = 0.00,
                Bonus = 0
            };
            _userPairs = new List<Pair>();
            return View(await _context.Ticket.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            var ticketMatches = new TicketDetailsViewModel
            {
                Wager = ticket.Wager,
                PossibleGain = ticket.PossibleGain
            };

            var pairDetails = new List<PairDetails>();
            var pairIds = ticket.Pairs.Split(',').Select(int.Parse).ToList();

            foreach (var pairId in pairIds)
            {
                var pair = _context.Pair
                .SingleOrDefault(m => m.Id == pairId);

                var match = _context.Match.SingleOrDefault(m => m.Id == pair.MatchId);

                pairDetails.Add(new PairDetails
                {
                    Pair = pair,
                    Match = match
                });
            }

            ticketMatches.PairDetails = pairDetails;

            return View(ticketMatches);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Ticket.SingleOrDefaultAsync(m => m.Id == id);
            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Tickets/UpdatePairs/<match_id>_1
        // POST: Tickets/UpdatePairs/<match_id>_X
        // POST: Tickets/UpdatePairs/<match_id>_2
        public string UpdatePairs(string matchIdBet)
        {
            // Update list of unsubmitted tickets
            if (matchIdBet == null)
            {
                UpdateNewTicketData();
                return "fail";
            }

            var data = matchIdBet.Split('_');
            // parse match id
            if (!int.TryParse(data[0], out var matchId))
            {
                UpdateNewTicketData();
                return "fail";
            }


            var p = _userPairs.Find(f => f.MatchId == matchId);

            // User deselects the pair he selected previously
            if (p != null && p.Bet == Pair.GetBetType(data[1]))
            {
                _userPairs.Remove(p);
            }
            // User bets on same match but changes the bet (1/X/2)
            else if (p != null)
            {
                _userPairs.Remove(p);
                // Create new Pair instance and add it to the list   
                var pNew = _context.Pair.SingleOrDefault(f => f.MatchId == matchId && f.Bet == Pair.GetBetType(data[1]));
                _userPairs.Add(pNew);
            }
            // User bets on a new match
            else
            {
                // Obtain Pair instance from DB and add it to the list of user selected pairs 
                var pNew = _context.Pair.SingleOrDefault(f => f.MatchId == matchId && f.Bet == Pair.GetBetType(data[1]));
                _userPairs.Add(pNew);
            }

            // Inform client of successful post action
            UpdateNewTicketData();
            return "success";
        }

        // POST: Tickets/WagerUpdate/<new_wager_value>
        public string WagerUpdate(int value)
        {
            NewTicketData.Wager = value;
            UpdateNewTicketData();
            return "success";
        }

        // POST: Tickets/Submit
        public string Submit()
        {

            var ticket = new Ticket
            {
                Wager = NewTicketData.Wager,
                PossibleGain = (int)NewTicketData.PossibleGain,
                Pairs = Pair.StringifyPairs(_userPairs)
            };

            _context.Add(ticket);
            _context.SaveChanges();

            UpdateWalletState();
            System.Threading.Thread.Sleep(500);
            ResetNewTicketData();
            _userPairs = new List<Pair>();

            return "Home";
        }

        // GET Tickets/RefreshNewTicketData
        public IActionResult RefreshNewTicketDataView()
        {
            // Update client-side view
            return PartialView("_NewTicketSubmit", NewTicketData);
        }


        // --------------- HELPERS ---------------

        public double GetBetAmount(Match match, BetType t)
        {
            if (t == BetType.BetHome)
                return match.HomeWins;
            else if (t == BetType.BetTied)
                return match.Tied;
            else
                return match.AwayWins;
        }

        public async void UpdateWalletState()
        {
            var walletState = WalletTransactionsController.GetWalletState(_context);
            var transaction = new WalletTransaction
            {
                WalletBefore = walletState,
                WalletAfter = walletState - NewTicketData.Wager,
                TransactionDate = DateTime.Now
            };
            await new WalletTransactionsController(_context).Create(transaction);
        }

        // Calculate possible gain based on wager entered and selected pairs
        public double CalculatePossibleGain(int wager, int bonus)
        {
            var gain = (double)wager;

            if (!_userPairs.Any())
                return 0;

            foreach (var pair in _userPairs.ToList())
            {
                var match = _context.Match
                .SingleOrDefault(m => m.Id == pair.MatchId);

                gain *= GetBetAmount(match, pair.Bet);
            }

            return gain + bonus;
        }

        // Add bonuses on possible gain sum to user:
        // - Add 5 if user bet on three pairs from same sport
        // - Add 10 if user bet on at least one pair from all sports
        public int CalculateBonus()
        {
            var userMatches = new List<Match>();
            var allMatches = _context.Match.ToList();
            var bonus = 0;

            foreach (var pair in _userPairs.ToList())
            {
                var match = _context.Match
                .SingleOrDefault(m => m.Id == pair.MatchId);

                userMatches.Add(match);
            }

            var countSameSport = (from m in userMatches
                                  group m by m.Sport into g
                                  let count = g.Count()
                                  orderby count descending
                                  select count).FirstOrDefault();

            if (countSameSport >= 3) 
                bonus += 5;

            var countAllSports = (from m in allMatches
                                  group m by m.Sport into g
                                  select g).Count();

            var countUserSports = (from m in userMatches
                                   group m by m.Sport into g
                                   select g).Count();

            if (countAllSports == countUserSports)
                bonus += 10;

            return bonus; 
        }

        public void UpdateNewTicketData()
        {
            NewTicketData.Wallet = WalletTransactionsController.GetWalletState(_context);
            NewTicketData.Bonus = CalculateBonus();
            NewTicketData.PossibleGain = CalculatePossibleGain(NewTicketData.Wager, NewTicketData.Bonus);
        }

        public void ResetNewTicketData()
        {
            NewTicketData.Wallet = WalletTransactionsController.GetWalletState(_context);
            NewTicketData.Bonus = 0;
            NewTicketData.PossibleGain = 0;
        }
    }
}
