using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinic.Data;
using Clinic.Models;

namespace Clinic.Controllers
{
    public class SchedulingsController : Controller
    {
        private readonly ClinicContext _context;

        public SchedulingsController(ClinicContext context)
        {
            _context = context;
        }

        // GET: Schedulings
        public async Task<IActionResult> Index()
        {
            var clinicContext = _context.Scheduling.Include(s => s.Patient).Include(s => s.Term);
            return View(await clinicContext.ToListAsync());
        }

        // GET: Schedulings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduling = await _context.Scheduling
                .Include(s => s.Patient)
                .Include(s => s.Term)
                .FirstOrDefaultAsync(m => m.ScheduleID == id);
            if (scheduling == null)
            {
                return NotFound();
            }

            return View(scheduling);
        }

        // GET: Schedulings/Create
        public IActionResult Create()
        {
            ViewData["PatientID"] = new SelectList(_context.Patients, "Id", "FirstName");
            ViewData["TermID"] = new SelectList(_context.Terms, "TermID", "Category");
            return View();
        }

        // POST: Schedulings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleID,TermID,PatientID,Diagnose")] Scheduling scheduling)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scheduling);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientID"] = new SelectList(_context.Patients, "Id", "FirstName", scheduling.PatientID);
            ViewData["TermID"] = new SelectList(_context.Terms, "TermID", "Category", scheduling.TermID);
            return View(scheduling);
        }

        // GET: Schedulings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduling = await _context.Scheduling.FindAsync(id);
            if (scheduling == null)
            {
                return NotFound();
            }
            ViewData["PatientID"] = new SelectList(_context.Patients, "Id", "FirstName", scheduling.PatientID);
            ViewData["TermID"] = new SelectList(_context.Terms, "TermID", "Category", scheduling.TermID);
            return View(scheduling);
        }

        // POST: Schedulings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleID,TermID,PatientID,Diagnose")] Scheduling scheduling)
        {
            if (id != scheduling.ScheduleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scheduling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchedulingExists(scheduling.ScheduleID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientID"] = new SelectList(_context.Patients, "Id", "FirstName", scheduling.PatientID);
            ViewData["TermID"] = new SelectList(_context.Terms, "TermID", "Category", scheduling.TermID);
            return View(scheduling);
        }

        // GET: Schedulings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduling = await _context.Scheduling
                .Include(s => s.Patient)
                .Include(s => s.Term)
                .FirstOrDefaultAsync(m => m.ScheduleID == id);
            if (scheduling == null)
            {
                return NotFound();
            }

            return View(scheduling);
        }

        // POST: Schedulings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scheduling = await _context.Scheduling.FindAsync(id);
            _context.Scheduling.Remove(scheduling);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchedulingExists(int id)
        {
            return _context.Scheduling.Any(e => e.ScheduleID == id);
        }
    }
}
