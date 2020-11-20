using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinic.Data;
using Clinic.Models;
using Clinic.ViewModels;

namespace Clinic.Controllers
{
    public class TermsController : Controller
    {
        private readonly ClinicContext _context;

        public TermsController(ClinicContext context)
        {
            _context = context;
        }

        // GET: Terms
        public async Task<IActionResult> Index(bool termPriority, string searchString)
        {
            IQueryable<Terms> terms = _context.Terms.AsQueryable();
            IQueryable<bool> priorityQuery = _context.Terms.OrderBy(m => m.Priority).Select(m => m.Priority).Distinct();
            if (!string.IsNullOrEmpty(searchString))
            {
                terms = terms.Where(s => s.Category.Contains(searchString));
            }
            if (termPriority==true)
            {
                terms = terms.Where(x => x.Priority == termPriority);
            }
            terms = terms.Include(m => m.Doctor)
            .Include(m => m.Patients).ThenInclude(m => m.Patient);
            var termPriorityVM = new TermPriorityViewModel
            {
                Priorities = new SelectList(await priorityQuery.ToListAsync()),
                Terms = await terms.ToListAsync()
            };
            return View(termPriorityVM);
        }

        // GET: Terms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terms = await _context.Terms
                .Include(t => t.Doctor)
                .Include(m => m.Patients).ThenInclude(m => m.Patient)
                .FirstOrDefaultAsync(m => m.TermID == id);
            if (terms == null)
            {
                return NotFound();
            }

            return View(terms);
        }

        // GET: Terms/Create
        public IActionResult Create()
        {
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "FullName");
            return View();
        }

        // POST: Terms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TermID,Category,Date,Priority,DoctorId")] Terms terms)
        {
            if (ModelState.IsValid)
            {
                _context.Add(terms);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "FullName", terms.DoctorId);
            return View(terms);
        }

        // GET: Terms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terms = _context.Terms.Where(m => m.TermID == id).Include(m => m.Patients).First();
            if (terms == null)
            {
                return NotFound();
            }
            TermPatientEditViewModel viewmodel = new TermPatientEditViewModel
            {
                Term = terms,
                PatientList = new MultiSelectList(_context.Patients.OrderBy(s => s.FullName), "Id", "FullName"),
                SelectedPatients = terms.Patients.Select(sa => sa.PatientID)
            };
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "FullName", terms.DoctorId);
            return View(viewmodel);
        }

        // POST: Terms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TermPatientEditViewModel viewmodel)
        {
            if (id != viewmodel.Term.TermID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                     
                        _context.Update(viewmodel.Term);
                        await _context.SaveChangesAsync();
                        IEnumerable<int> listPatients = viewmodel.SelectedPatients;
                        IQueryable<PatientTerm> toBeRemoved = _context.PatientTerm.Where(s => !listPatients.Contains(s.PatientId) && s.TermId == id);
                        _context.PatientTerm.RemoveRange(toBeRemoved);
                        IEnumerable<int> existPatients = _context.PatientTerm.Where(s => listPatients.Contains(s.PatientId) && s.TermId == id).Select(s => s.PatientId);
                        IEnumerable<int> newPatients = listPatients.Where(s => !existPatients.Contains(s));
                        foreach (int patientId in newPatients)
                            _context.PatientTerm.Add(new PatientTerm { PatientId = patientId, TermId = id });
                    }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TermsExists(viewmodel.Term.TermID))
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
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "FullName", viewmodel.Term.DoctorId);
            return View(viewmodel);
        }

        // GET: Terms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terms = await _context.Terms
                .Include(t => t.Doctor)
                .Include(m => m.Patients).ThenInclude(m => m.Patient)
                .FirstOrDefaultAsync(m => m.TermID == id);
            if (terms == null)
            {
                return NotFound();
            }

            return View(terms);
        }

        // POST: Terms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var terms = await _context.Terms.FindAsync(id);
            _context.Terms.Remove(terms);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TermsExists(int id)
        {
            return _context.Terms.Any(e => e.TermID == id);
        }
    }
}
