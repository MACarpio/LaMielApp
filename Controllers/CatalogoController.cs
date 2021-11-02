using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaMielApp.Models;
using LaMielApp.Data;

namespace LaMielApp.Controllers
{
    public class CatalogoController: Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public CatalogoController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

         public async Task<IActionResult> Index()
        {
            var productos = from o in _context.DataProduct select o;
            return View(await productos.ToListAsync());
        }
        
        [HttpGet]
        public async Task<IActionResult> Index(String Empsearch){
            ViewData["Getemployeedetails"]=Empsearch;
            var empquery=from x in _context.DataProduct select x;
            if(!string.IsNullOrEmpty(Empsearch)){
                empquery=empquery.Where(x =>x.Name.Contains(Empsearch))  ;
            }
            return View(await empquery.AsNoTracking().ToListAsync());

        }
        public async Task<IActionResult> Details(int? id)
        {
            Product objProduct = await _context.DataProduct.FindAsync(id);
            if(objProduct == null){
                return NotFound();
            }
            return View(objProduct);
        }


          public async Task<IActionResult> Add(int? id)
        {
            var userID = _userManager.GetUserName(User);
            if(userID == null){
                ViewData["Message"] = "Por favor debe loguearse antes de agregar un producto";
                List<Product> productos = new List<Product>();
                return  View("Index",productos);
            }else{
                var producto = await _context.DataProduct.FindAsync(id);
                Proforma proforma = new Proforma();
                proforma.Producto = producto;
                proforma.Price = producto.Price;
                proforma.Quantity = 1;
                proforma.UserID = userID;
                _context.Add(proforma);
                await _context.SaveChangesAsync();
                return  RedirectToAction(nameof(Index));
            }
        }
    }
}