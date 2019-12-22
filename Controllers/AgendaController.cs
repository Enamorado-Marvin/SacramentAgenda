using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SacramentAgenda.Data;
using SacramentAgenda.Models;

namespace SacramentAgenda.Controllers
{
    public class AgendaController : Controller
    {
        private readonly UnitContext _context;

        public AgendaController(UnitContext context)
        {
            _context = context;
        }

        // GET: Agenda
        public async Task<IActionResult> Index(
        string sortOrder,
        string currentFilter,
        string searchString,
        int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var agendas = from s in _context.Agendas
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                agendas = agendas.Where(s => s.ConductingLeader.Contains(searchString)
                                       || s.PresidingLeader.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    agendas = agendas.OrderByDescending(s => s.PresidingLeader);
                    break;
                case "Date":
                    agendas = agendas.OrderBy(s => s.SacramentMeetingDate);
                    break;
                case "date_desc":
                    agendas = agendas.OrderByDescending(s => s.ConductingLeader);
                    break;
                default:
                    agendas = agendas.OrderBy(s => s.SacramentMeetingDate);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Agenda>.CreateAsync(agendas.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Agenda/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agenda = await _context.Agendas
                .Include(s => s.Assignments)
                .ThenInclude(e => e.Speaker)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (agenda == null)
            {
                return NotFound();
            }

            return View(agenda);
        }

        // GET: Agenda/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agenda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SacramentMeetingDate,PresidingLeader,ConductingLeader,OpeningSong,Invocation,SpecialNumber,ClosingSong,Benediction")] Agenda agenda)
        {
            try
            {
            if (ModelState.IsValid)
                {
                    _context.Add(agenda);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(agenda);
        }

        // GET: Agenda/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agenda = await _context.Agendas.FindAsync(id);
            if (agenda == null)
            {
                return NotFound();
            }
            return View(agenda);
        }

        // POST: Agenda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var agendaToUpdate = await _context.Agendas.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Agenda>(
                agendaToUpdate,
                "",
                s => s.SacramentMeetingDate, s => s.PresidingLeader, s => s.ConductingLeader, s=> s.OpeningSong, s=> s.Invocation, s=> s.SpecialNumber, s=> s.ClosingSong, s=> s.Benediction))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(agendaToUpdate);
        }

        // GET: Agenda/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agenda = await _context.Agendas
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (agenda == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(agenda);
        }

        // POST: Agenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agenda = await _context.Agendas.FindAsync(id);
            if (agenda == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Agendas.Remove(agenda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool AgendaExists(int id)
        {
            return _context.Agendas.Any(e => e.ID == id);
        }
    }
}
