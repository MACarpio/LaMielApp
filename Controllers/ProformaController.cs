using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaMielApp.Data;
using LaMielApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Dynamic;
using Rotativa.AspNetCore;

namespace AngelaStoreApp.Controllers
{
public class ProformaController: Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProformaController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
               // GET: Proforma
        public async Task<IActionResult> Index()
        {
            var userID = _userManager.GetUserName(User);
            var items = from o in _context.DataProforma select o;
            items = items.
                Include(p => p.Producto).
                Where(s => s.UserID.Equals(userID));

            var elements = await items.ToListAsync();
            var total = elements.Sum(c => c.Quantity * c.Price );
            
            dynamic model = new ExpandoObject();
            model.montoTotal = total;
            model.proformas = elements;

            return View(model);
        }

         // GET: Proforma/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proforma = await _context.DataProforma.FindAsync(id);
            _context.DataProforma.Remove(proforma);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Proforma/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proforma = await _context.DataProforma.Include(p => p.Producto)
            .FirstOrDefaultAsync(i => i.Id == id);
            if (proforma == null)
            {
                return NotFound();
            }
            return View(proforma);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantity,Price,UserID")] Proforma proforma)
        {
            if (id != proforma.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proforma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProformaExists(proforma.Id))
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
            return View(proforma);
        }
        private bool ProformaExists(int id)
        {
            return _context.DataProforma.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Documento(int id)
        {
           // return View(await _context.Documento.ToListAsync());
            var userID = _userManager.GetUserName(User);
            var items = from o in _context.DataProforma select o;
            items = items.
                Include(p => p.Producto).
                Where(s => s.UserID.Equals(userID));
            var elements = await items.ToListAsync();
            var total = elements.Sum(c => c.Quantity * c.Price );
            var pago = await _context.DataPago.FindAsync(id);
            
            dynamic model = new ExpandoObject();
            model.montoTotal = total;
            model.proformas = elements;
            model.pago = pago;
           return new ViewAsPdf("Documento", model);
        }
    }
}