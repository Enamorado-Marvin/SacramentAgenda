using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SacramentAgenda.Models;
using Microsoft.EntityFrameworkCore;
using SacramentAgenda.Data;
using SacramentAgenda.Models.UnitViewModels;

namespace SacramentAgenda.Controllers
{
    public class HomeController : Controller
    {
        private readonly UnitContext _context;

        public HomeController(UnitContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> About()
        {
            IQueryable<AssignmentDateGroup> data =
                from agenda in _context.Agendas
                group agenda by agenda.SacramentMeetingDate into dateGroup
                select new AssignmentDateGroup()
                {
                    AssignmentDate = dateGroup.Key,
                    AgendaCount = dateGroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());
        }
        public IActionResult Index()
        {
            return View();
        }        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
