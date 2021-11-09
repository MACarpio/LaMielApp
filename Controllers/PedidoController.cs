using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LaMielApp.Models;
using LaMielApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Rotativa.AspNetCore;

namespace LaMielApp.Controllers
{
    public class PedidoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PedidoController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var pedidos = from o in _context.DataPedido select o;
            return View(await pedidos.Include(p => p.pago).ToListAsync());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> Index(String EmpsearchId, String EmpsearchUsu, String EmpsearchEst)
        {
            ViewData["Getemployeedetails"] = EmpsearchId;
            ViewData["Getemployeedetails1"] = EmpsearchUsu;
            ViewData["Getemployeedetails2"] = EmpsearchEst;
            var empquery = from x in _context.DataPedido select x;
            if (!string.IsNullOrEmpty(EmpsearchId))
            {
                int numVal = Int32.Parse(EmpsearchId);
                empquery = empquery.Where(x => x.ID.Equals(numVal));
            }
            if (!string.IsNullOrEmpty(EmpsearchUsu))
            {
                empquery = empquery.Where(x => x.UserID.Contains(EmpsearchUsu));
                if (EmpsearchEst != "TODOS")
                {
                    empquery = empquery.Where(x => x.Status.Equals(EmpsearchEst));
                }
            }
            if (!string.IsNullOrEmpty(EmpsearchEst))
            {
                if (EmpsearchEst != "TODOS")
                {
                    empquery = empquery.Where(x => x.Status.Equals(EmpsearchEst));
                }
            }
            return View(await empquery.Include(p => p.pago).AsNoTracking().ToListAsync());
        }

        // GET: Proforma/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.DataPedido.Include(p => p.pago)
            .FirstOrDefaultAsync(i => i.ID == id);
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }
        [Authorize(Roles = "Admin")]
        private bool PedidoExists(int id)
        {
            return _context.DataPedido.Any(e => e.ID == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Pedido pedido)
        {
            if (id != pedido.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.ID))
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
            return View(pedido);
        }
        public async Task<IActionResult> Reporte(String EmpsearchId, String EmpsearchUsu, String EmpsearchEst)
        {
            ViewData["Getemployeedetails"] = EmpsearchId;
            ViewData["Getemployeedetails1"] = EmpsearchUsu;
            ViewData["Getemployeedetails2"] = EmpsearchEst;
            var empquery = from x in _context.DataPedido select x;
            if (!string.IsNullOrEmpty(EmpsearchId))
            {
                int numVal = Int32.Parse(EmpsearchId);
                empquery = empquery.Where(x => x.ID.Equals(numVal));
            }
            if (!string.IsNullOrEmpty(EmpsearchUsu))
            {
                empquery = empquery.Where(x => x.UserID.Contains(EmpsearchUsu));
                if (EmpsearchEst != "TODOS")
                {
                    empquery = empquery.Where(x => x.Status.Equals(EmpsearchEst));
                }
            }
            if (!string.IsNullOrEmpty(EmpsearchEst))
            {
                if (EmpsearchEst != "TODOS")
                {
                    empquery = empquery.Where(x => x.Status.Equals(EmpsearchEst));
                }
            }
            return new ViewAsPdf("Reporte", await empquery.Include(p => p.pago).AsNoTracking().ToListAsync());
        }
    }
}
