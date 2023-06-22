using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using chinliuliu.Models;
using chinliuliu.Models.Process;
namespace chinliuliu.Controllers
{
    public class SinhvienController : Controller
    {
        private readonly MvcMovieContext _context;
        private StringProcess strPro = new StringProcess();
        private ExcelProcess _excelProcess = new ExcelProcess();

        public SinhvienController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Sinhvien
        public async Task<IActionResult> Index()
        {
              return _context.Sinhvien != null ? 
                          View(await _context.Sinhvien.ToListAsync()) :
                          Problem("Entity set 'MvcMovieContext.Sinhvien'  is null.");
        }

        // GET: Sinhvien/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Sinhvien == null)
            {
                return NotFound();
            }

            var sinhvien = await _context.Sinhvien
                .FirstOrDefaultAsync(m => m.Masinhvien == id);
            if (sinhvien == null)
            {
                return NotFound();
            }

            return View(sinhvien);
        }

        // GET: Sinhvien/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sinhvien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Masinhvien,Hoten,Malop")] Sinhvien sinhvien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sinhvien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sinhvien);
        }

        // GET: Sinhvien/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Sinhvien == null)
            {
                return NotFound();
            }

            var sinhvien = await _context.Sinhvien.FindAsync(id);
            if (sinhvien == null)
            {
                return NotFound();
            }
            return View(sinhvien);
        }

        // POST: Sinhvien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Masinhvien,Hoten,Malop")] Sinhvien sinhvien)
        {
            if (id != sinhvien.Masinhvien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sinhvien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SinhvienExists(sinhvien.Masinhvien))
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
            return View(sinhvien);
        }

        // GET: Sinhvien/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Sinhvien == null)
            {
                return NotFound();
            }

            var sinhvien = await _context.Sinhvien
                .FirstOrDefaultAsync(m => m.Masinhvien == id);
            if (sinhvien == null)
            {
                return NotFound();
            }

            return View(sinhvien);
        }

        // POST: Sinhvien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Sinhvien == null)
            {
                return Problem("Entity set 'MvcMovieContext.Sinhvien'  is null.");
            }
            var sinhvien = await _context.Sinhvien.FindAsync(id);
            if (sinhvien != null)
            {
                _context.Sinhvien.Remove(sinhvien);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SinhvienExists(string id)
        {
          return (_context.Sinhvien?.Any(e => e.Masinhvien == id)).GetValueOrDefault();
        }
         public async Task<IActionResult> Upload()
        {
            return View();
        }
    [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    ModelState.AddModelError("", "Please choose excel file to upload!");
                }
                else
                {
                    var FileName = DateTime.Now.ToShortTimeString() + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels", FileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        //save file to sever
                        await file.CopyToAsync(stream);
                        var dt = _excelProcess.ExcelToDataTable(fileLocation);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var sv = new Sinhvien();

                            sv.Masinhvien = dt.Rows[i][0].ToString();
                            sv.Hoten = dt.Rows[i][1].ToString();
                            sv.Malop = dt.Rows[i][2].ToString();
                           
                         _context.Sinhvien.Add(sv);
                        }
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
    }
    }
}
