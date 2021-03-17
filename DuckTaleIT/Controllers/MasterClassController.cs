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
    public class MasterClassController : Controller
    {
        private readonly DataContext _context;

        public MasterClassController(DataContext context)
        {
            _context = context;
        }

        // GET: MasterClass
        public async Task<IActionResult> Index()
        {
            return View(await _context.MasterClasses.Where(s=>s.IsActive==true).ToListAsync());
        }


        // GET: MasterClass/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MasterClass/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("mClassId,mClassName,mClassCode,IsActive")] MasterClass masterClass)
        {
            masterClass.IsActive = true;
            if (ModelState.IsValid)
            {
                _context.Add(masterClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(masterClass);
        }

        // GET: MasterClass/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masterClass = await _context.MasterClasses.FindAsync(id);
            if (masterClass == null || masterClass.IsActive==false)
            {
                return NotFound();
            }
            return View(masterClass);
        }

        // POST: MasterClass/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("mClassId,mClassName,mClassCode,IsActive")] MasterClass masterClass)
        {
            if (id != masterClass.mClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(masterClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MasterClassExists(masterClass.mClassId))
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
            return View(masterClass);
        }

     

        // POST: MasterClass/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var masterClass = await _context.MasterClasses.FindAsync(id);
            masterClass.IsActive = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MasterClassExists(int id)
        {
            return _context.MasterClasses.Any(e => e.mClassId == id);
        }
    }
}
