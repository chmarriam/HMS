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
    public class PrescriptionItemController : Controller
    {
        private readonly NeondbContext _context;

        public PrescriptionItemController(NeondbContext context)
        {
            _context = context;
        }

        // GET: PrescriptionItem
        public async Task<IActionResult> Index()
        {
            var neondbContext = _context.PrescriptionItems.Include(p => p.Medication).Include(p => p.Prescription);
            return View(await neondbContext.ToListAsync());
        }

        // GET: PrescriptionItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescriptionItem = await _context.PrescriptionItems
                .Include(p => p.Medication)
                .Include(p => p.Prescription)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (prescriptionItem == null)
            {
                return NotFound();
            }

            return View(prescriptionItem);
        }

        // GET: PrescriptionItem/Create
        public IActionResult Create()
        {
            ViewData["MedicationId"] = new SelectList(_context.Medications, "MedicationId", "MedicationId");
            ViewData["PrescriptionId"] = new SelectList(_context.Prescriptions, "PrescriptionId", "PrescriptionId");
            return View();
        }

        // POST: PrescriptionItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,PrescriptionId,MedicationId,Quantity,Instructions")] PrescriptionItem prescriptionItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prescriptionItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicationId"] = new SelectList(_context.Medications, "MedicationId", "MedicationId", prescriptionItem.MedicationId);
            ViewData["PrescriptionId"] = new SelectList(_context.Prescriptions, "PrescriptionId", "PrescriptionId", prescriptionItem.PrescriptionId);
            return View(prescriptionItem);
        }

        // GET: PrescriptionItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescriptionItem = await _context.PrescriptionItems.FindAsync(id);
            if (prescriptionItem == null)
            {
                return NotFound();
            }
            ViewData["MedicationId"] = new SelectList(_context.Medications, "MedicationId", "MedicationId", prescriptionItem.MedicationId);
            ViewData["PrescriptionId"] = new SelectList(_context.Prescriptions, "PrescriptionId", "PrescriptionId", prescriptionItem.PrescriptionId);
            return View(prescriptionItem);
        }

        // POST: PrescriptionItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,PrescriptionId,MedicationId,Quantity,Instructions")] PrescriptionItem prescriptionItem)
        {
            if (id != prescriptionItem.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prescriptionItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrescriptionItemExists(prescriptionItem.ItemId))
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
            ViewData["MedicationId"] = new SelectList(_context.Medications, "MedicationId", "MedicationId", prescriptionItem.MedicationId);
            ViewData["PrescriptionId"] = new SelectList(_context.Prescriptions, "PrescriptionId", "PrescriptionId", prescriptionItem.PrescriptionId);
            return View(prescriptionItem);
        }

        // GET: PrescriptionItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescriptionItem = await _context.PrescriptionItems
                .Include(p => p.Medication)
                .Include(p => p.Prescription)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (prescriptionItem == null)
            {
                return NotFound();
            }

            return View(prescriptionItem);
        }

        // POST: PrescriptionItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prescriptionItem = await _context.PrescriptionItems.FindAsync(id);
            if (prescriptionItem != null)
            {
                _context.PrescriptionItems.Remove(prescriptionItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrescriptionItemExists(int id)
        {
            return _context.PrescriptionItems.Any(e => e.ItemId == id);
        }
    }
}
