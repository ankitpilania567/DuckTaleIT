using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DuckTaleIT.Models;

namespace DuckTaleIT.Controllers
{
    public class MasterSubjectController : Controller
    {
        private readonly DataContext _context;

        public MasterSubjectController(DataContext context)
        {
            _context = context;
        }

        // GET: MasterSubject
        public async Task<IActionResult> Index()
        {
            return View(await _context.MasterSubjects.Where(s=>s.IsActive==true).ToListAsync());
        }

      

        // GET: MasterSubject/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MasterSubject/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("mSubjectId,mSubjectName,mSubjectCode,IsActive")] MasterSubject masterSubject)
        {
            masterSubject.IsActive = true;
            if (ModelState.IsValid)
            {
                _context.Add(masterSubject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(masterSubject);
        }

        // GET: MasterSubject/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masterSubject = await _context.MasterSubjects.FindAsync(id);
            if (masterSubject == null || masterSubject.IsActive==false)
            {
                return NotFound();
            }
            return View(masterSubject);
        }

        // POST: MasterSubject/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("mSubjectId,mSubjectName,mSubjectCode,IsActive")] MasterSubject masterSubject)
        {
            if (id != masterSubject.mSubjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(masterSubject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MasterSubjectExists(masterSubject.mSubjectId))
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
            return View(masterSubject);
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            var masterSubject = await _context.MasterSubjects.FindAsync(id);
            masterSubject.IsActive = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MasterSubjectExists(int id)
        {
            return _context.MasterSubjects.Any(e => e.mSubjectId == id);
        }
    }
}
