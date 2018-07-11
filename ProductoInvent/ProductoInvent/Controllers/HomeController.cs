using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductoInvent.Models;
using ProductoInvent.Shared;

namespace ProductoInvent.Controllers
{
    public class HomeController : Controller
    {
        private ProductoInventRepository productoInventRepository;
        private AzureStorageBlob azureStorageblob;
        public HomeController()
        {
            productoInventRepository = new ProductoInventRepository();
            azureStorageblob = new AzureStorageBlob();
        }

        public IActionResult Index()
        {
            var result = productoInventRepository.GetProductCollectionsWithImage();
            result.ForEach(x =>
            {
                x.ProductImage = azureStorageblob.DownloadFromBlob(x.FileName).Result == null ?
                azureStorageblob.DownloadFromBlob("NoImage.png").Result : azureStorageblob.DownloadFromBlob(x.FileName).Result;
            });            
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
