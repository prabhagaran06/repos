using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductoInvent.Models;

namespace ProductoInvent.Controllers
{
    public class HomeController : Controller
    {
        private ProductoInventRepository productoInventRepository;
        public HomeController()
        {
            productoInventRepository = new ProductoInventRepository();
        }

        public IActionResult Index()
        {
            var result = productoInventRepository.GetProductCollectionsWithImage();
            return View(result);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
