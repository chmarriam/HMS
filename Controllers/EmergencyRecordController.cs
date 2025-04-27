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
    public class EmergencyRecordController : Controller
    {
        private readonly NeondbContext _context;

        public EmergencyRecordController(NeondbContext context)
        {
            _context = context;
        }

        // GET: EmergencyRecord
        public async Task<IActionResult> Index()
        {
            var neondbContext = _context.EmergencyRecords.Include(e => e.Patient);
            return View(await neondbContext.ToListAsync());
        }

        // GET: EmergencyRecord/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emergencyRecord = await _context.EmergencyRecords
                .Include(e => e.Patient)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (emergencyRecord == null)
            {
                return NotFound();
            }

            return View(emergencyRecord);
        }

        // GET: EmergencyRecord/Create
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId");
            return View();
        }

        // POST: EmergencyRecord/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecordId,PatientId,ArrivalTime,Complaint,Diagnosis,Treatment")] EmergencyRecord emergencyRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emergencyRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", emergencyRecord.PatientId);
            return View(emergencyRecord);
        }

        // GET: EmergencyRecord/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emergencyRecord = await _context.EmergencyRecords.FindAsync(id);
            if (emergencyRecord == null)
            {
                return NotFound();
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", emergencyRecord.PatientId);
            return View(emergencyRecord);
        }

        // POST: EmergencyRecord/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecordId,PatientId,ArrivalTime,Complaint,Diagnosis,Treatment")] EmergencyRecord emergencyRecord)
        {
            if (id != emergencyRecord.RecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emergencyRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmergencyRecordExists(emergencyRecord.RecordId))
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
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", emergencyRecord.PatientId);
            return View(emergencyRecord);
        }

        // GET: EmergencyRecord/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emergencyRecord = await _context.EmergencyRecords
                .Include(e => e.Patient)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (emergencyRecord == null)
            {
                return NotFound();
            }

            return View(emergencyRecord);
        }

        // POST: EmergencyRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emergencyRecord = await _context.EmergencyRecords.FindAsync(id);
            if (emergencyRecord != null)
            {
                _context.EmergencyRecords.Remove(emergencyRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmergencyRecordExists(int id)
        {
            return _context.EmergencyRecords.Any(e => e.RecordId == id);
        }
    }
}
