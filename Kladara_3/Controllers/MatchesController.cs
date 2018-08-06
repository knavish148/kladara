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
            return View(_context.Match.ToList());
        }
    }
}
