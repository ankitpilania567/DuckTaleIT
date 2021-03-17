using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DuckTaleIT.Models;
using Newtonsoft;
using Newtonsoft.Json;

namespace DuckTaleIT.Controllers
{
    public class MasterStudentController : Controller
    {
        private readonly DataContext _context;

        public MasterStudentController(DataContext context)
        {
            _context = context;
        }

        // GET: MasterStudent
        public async Task<IActionResult> Index(int PageNo,string Query,string filterBy)
        {
            if (string.IsNullOrEmpty(filterBy))
            {
                filterBy = "Name";
            }
            if (string.IsNullOrEmpty(Query))
            {
                Query = "";
            }
            int pageSize = 5;
            var dataContext = _context.MasterStudents.Include(m => m.masterClass).Select(s=>new StudentList() { masterStudent=s, subjectStudentWise=_context.SubjectStudentWises.Include(c=>c.masterSubject).Where(c=>c.sswStudentId==s.StuId)});
            var Data1 = dataContext;
            switch(filterBy)
            {
                case "Name":
                    Data1 = dataContext.Where(s => s.masterStudent.FirstName.ToLower().Contains(Query.ToLower()) || s.masterStudent.LastName.ToLower().Contains(Query.ToLower()));
                    break;
                case "Class":
                    Data1 = dataContext.Where(s => s.masterStudent.masterClass.mClassName.ToLower().Contains(Query.ToLower()));
                    break;
                case "Subject":
                    Data1 = dataContext.Where(s => (s.subjectStudentWise.Any(c=>c.masterSubject.mSubjectName.ToLower().Contains(Query.ToLower()))));
                    break;
            }
            var pageCount = Math.Ceiling((double)Data1.Count() / pageSize);
            if (PageNo == 0)
            {
                PageNo = 1;
            }
            var skip = (PageNo - 1) * pageSize;
            ViewBag.PageNo = PageNo;
            ViewBag.filterBy = filterBy;
            ViewBag.Query = Query;
            ViewBag.pageCount = pageCount;
            var data= await Data1.Skip(skip).Take(pageSize).ToListAsync();
            return View(data);
        }

       

        // GET: MasterStudent/Create
        public IActionResult Create()
        {
            ViewData["mClassName"] = new SelectList(_context.MasterClasses.Where(s => s.IsActive == true), "mClassId", "mClassName");
            ViewBag.SubjectList = _context.MasterSubjects.Where(s => s.IsActive == true).ToList();
            return View();
        }

        // POST: MasterStudent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StuId,FirstName,LastName,mClassName")] MasterStudent masterStudent,string SelSubjectList)
        {

            if (ModelState.IsValid)
            {
                _context.Add(masterStudent);
                if (!string.IsNullOrEmpty(SelSubjectList))
                {
                    List<SubjectList> list = JsonConvert.DeserializeObject<List<SubjectList>>(SelSubjectList);
                    List<SubjectStudentWise> lst = list.Select(s => new SubjectStudentWise() { sswStudentId= masterStudent.StuId,sswSubjectId=s.id,sswStudentMarks=Convert.ToDecimal(s.marks) }).ToList();
                    _context.SubjectStudentWises.AddRange(lst);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
             
            ViewData["mClassName"] = new SelectList(_context.MasterClasses, "mClassId", "mClassName", masterStudent.mClassName);
            ViewBag.SubjectList = _context.MasterSubjects.Where(s => s.IsActive == true).ToList();
            ViewBag.SelSubjectList = SelSubjectList;
            return View(masterStudent);
        }

        // GET: MasterStudent/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masterStudent = await _context.MasterStudents.FindAsync(id);
            if (masterStudent == null)
            {
                return NotFound();
            }
            var SubjectData = _context.SubjectStudentWises.Where(s => s.sswStudentId == masterStudent.StuId).Select(s => new { id=s.sswSubjectId, marks=s.sswStudentMarks }).ToList();
            ViewBag.SelSubjectList = JsonConvert.SerializeObject(SubjectData).ToString();
            ViewData["mClassName"] = new SelectList(_context.MasterClasses.Where(s=>s.IsActive==true), "mClassId", "mClassName", masterStudent.mClassName);
            ViewBag.SubjectList = _context.MasterSubjects.Where(s => s.IsActive == true).ToList();
            return View(masterStudent);
        }

        // POST: MasterStudent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StuId,FirstName,LastName,mClassName")] MasterStudent masterStudent, string SelSubjectList)
        {
            if (id != masterStudent.StuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.SubjectStudentWises.RemoveRange(_context.SubjectStudentWises.Where(s => s.sswStudentId == masterStudent.StuId).ToList());
                    _context.Update(masterStudent);
                    if (!string.IsNullOrEmpty(SelSubjectList))
                    {
                        List<SubjectList> list = JsonConvert.DeserializeObject<List<SubjectList>>(SelSubjectList);
                        List<SubjectStudentWise> lst = list.Select(s => new SubjectStudentWise() { sswStudentId = masterStudent.StuId, sswSubjectId = s.id, sswStudentMarks = Convert.ToDecimal(s.marks) }).ToList();
                        _context.SubjectStudentWises.AddRange(lst);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MasterStudentExists(masterStudent.StuId))
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
            ViewData["mClassName"] = new SelectList(_context.MasterClasses.Where(s => s.IsActive == true), "mClassId", "mClassName", masterStudent.mClassName);
            ViewBag.SubjectList = _context.MasterSubjects.Where(s => s.IsActive == true).ToList();
            ViewBag.SelSubjectList = SelSubjectList;
            return View(masterStudent);
        }


        
        public async Task<IActionResult> Delete(int id)
        {
            var masterStudent = await _context.MasterStudents.FindAsync(id);
            _context.MasterStudents.Remove(masterStudent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MasterStudentExists(int id)
        {
            return _context.MasterStudents.Any(e => e.StuId == id);
        }
    }
  
}
public class SubjectList
{
    public int id { get; set; }
    public string marks { get; set; }
}
