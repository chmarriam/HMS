using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HMS.Models;

namespace HMS.Controllers
{
    public class LabResultController : Controller
    {
        private readonly NeondbContext _context;

        public LabResultController(NeondbContext context)
        {
            _context = context;
        }

        // GET: LabResult
        public async Task<IActionResult> Index()
        {
            var neondbContext = _context.LabResults.Include(l => l.Patient).Include(l => l.Test);
            return View(await neondbContext.ToListAsync());
        }

        // GET: LabResult/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labResult = await _context.LabResults
                .Include(l => l.Patient)
                .Include(l => l.Test)
                .FirstOrDefaultAsync(m => m.ResultId == id);
            if (labResult == null)
            {
                return NotFound();
            }

            return View(labResult);
        }

        // GET: LabResult/Create
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId");
            ViewData["TestId"] = new SelectList(_context.LabTests, "TestId", "TestId");
            return View();
        }

        // POST: LabResult/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResultId,PatientId,TestId,ResultDate,ResultValue")] LabResult labResult)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", labResult.PatientId);
            ViewData["TestId"] = new SelectList(_context.LabTests, "TestId", "TestId", labResult.TestId);
            return View(labResult);
        }

        // GET: LabResult/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labResult = await _context.LabResults.FindAsync(id);
            if (labResult == null)
            {
                return NotFound();
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", labResult.PatientId);
            ViewData["TestId"] = new SelectList(_context.LabTests, "TestId", "TestId", labResult.TestId);
            return View(labResult);
        }

        // POST: LabResult/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResultId,PatientId,TestId,ResultDate,ResultValue")] LabResult labResult)
        {
            if (id != labResult.ResultId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(labResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabResultExists(labResult.ResultId))
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
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", labResult.PatientId);
            ViewData["TestId"] = new SelectList(_context.LabTests, "TestId", "TestId", labResult.TestId);
            return View(labResult);
        }

        // GET: LabResult/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labResult = await _context.LabResults
                .Include(l => l.Patient)
                .Include(l => l.Test)
                .FirstOrDefaultAsync(m => m.ResultId == id);
            if (labResult == null)
            {
                return NotFound();
            }

            return View(labResult);
        }

        // POST: LabResult/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var labResult = await _context.LabResults.FindAsync(id);
            if (labResult != null)
            {
                _context.LabResults.Remove(labResult);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabResultExists(int id)
        {
            return _context.LabResults.Any(e => e.ResultId == id);
        }
    }
}
