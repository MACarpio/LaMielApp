using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaMielApp.Data;
using LaMielApp.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace LaMielApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        // GET: Product
        public async Task<IActionResult> Index()
        {
            return View(await _context.DataProduct.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.DataProduct
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Index(String Searchpro)
        {
            ViewData["Getemployeedetails"] = Searchpro;
            var empquery = from x in _context.DataProduct select x;
            if (!string.IsNullOrEmpty(Searchpro))
            {
                empquery = empquery.Where(x => x.Name.Contains(Searchpro));
            }
            return View(await empquery.AsNoTracking().ToListAsync());

        }
        // GET: Product/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Product product, List<IFormFile> upload)
        {
            if (ModelState.IsValid)
            {
                if (upload.Count > 0)
                {

                    foreach (var up in upload)
                    {
                        Stream str = up.OpenReadStream();
                        BinaryReader br = new BinaryReader(str);
                        Byte[] fileDet = br.ReadBytes((Int32)str.Length);
                        product.Imagen = fileDet;
                        product.ImagenName = Path.GetFileName(up.FileName);
                    }
                }
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.DataProduct.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Product product, List<IFormFile> upload)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (upload == null || upload.Count <= 0)
                    {
                        byte[] imagen = product.Imagen;
                        var nom = product.ImagenName;
                        product.Imagen = imagen;
                        product.ImagenName = nom;
                    }
                    else
                    {
                        foreach (var up in upload)
                        {
                            Stream str = up.OpenReadStream();
                            BinaryReader br = new BinaryReader(str);
                            Byte[] fileDet = br.ReadBytes((Int32)str.Length);
                            product.Imagen = fileDet;
                            product.ImagenName = Path.GetFileName(up.FileName);
                        }
                    }
                    ModelState.AddModelError("precio", "Solo valores numericos");
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Product/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.DataProduct
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.DataProduct.FindAsync(id);
            product.Status = "ELIMINADO";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.DataProduct.Any(e => e.Id == id);
        }
        public IActionResult MostrarImagen(int id)
        {
            var producto = _context.DataProduct.Find(id);
            byte[] imagen = producto.Imagen;
            return File(imagen, "img/png");
        }
    }
}
