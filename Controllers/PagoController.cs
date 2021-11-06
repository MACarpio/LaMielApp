using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaMielApp.Data;
using LaMielApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaMielApp.Controllers
{
    public class PagoController: Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public PagoController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
            {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Create(Decimal monto)
        {
             Pago pago = new Pago();
            pago.UserID = _userManager.GetUserName(User);
            pago.MontoTotal = monto;
            return View(pago);
        }

        [HttpPost]
        public IActionResult Pagar(Pago pago)
        {
            pago.PaymentDate = DateTime.Now;
            _context.Add(pago);

            var itemsProforma = from o in _context.DataProforma select o;
            itemsProforma = itemsProforma.
                Include(p => p.Producto).
                Where(s => s.UserID.Equals(pago.UserID) && s.Status.Equals("PENDIENTE"));

            Pedido pedido = new Pedido();
            pedido.UserID = pago.UserID;
            pedido.Total = pago.MontoTotal;
            pedido.pago = pago;
            pedido.Status = "PENDIENTE";
            _context.Add(pedido);


            List<DetallePedido> itemsPedido = new List<DetallePedido>();
            foreach(var item in itemsProforma.ToList()){
                DetallePedido detallePedido = new DetallePedido();
                detallePedido.pedido=pedido;
                detallePedido.Price = item.Price;
                detallePedido.Producto = item.Producto;
                detallePedido.Quantity = item.Quantity;
                itemsPedido.Add(detallePedido);

        }
        _context.AddRange(itemsPedido);

            foreach (Proforma p in itemsProforma.ToList())
            {
                p.Status="PROCESADO";
            }
            _context.UpdateRange(itemsProforma);

            _context.SaveChanges();
            ViewData["Message"] = "El pago se ha registrado";
            return View("Create");
        }

    }
}