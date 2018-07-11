using System;
using System.Web;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductoInvent.Models;
using ProductoInvent.Shared;
using System.IO;

namespace ProductoInvent
{
    public class ProductInventController : Controller
    {
        
        private ProductoInventRepository productoInventRepository;
        private AzureStorageBlob azureBlobStorage;
        private SendEmailLogic sendEmailLogic;
        public ProductInventController()
        {
            productoInventRepository = new ProductoInventRepository();
            azureBlobStorage = new AzureStorageBlob();
            sendEmailLogic = new SendEmailLogic();
        }
        // GET: ProductInvent
        public ActionResult Index()
        {
            List<ProductCollectionModel> productCollectionModels = productoInventRepository.GetProductCollections();
            return View(productCollectionModels);
        }

        // GET: ProductInvent/Details/5
        public ActionResult Details(int id)
        {
            var productcollectionmodel = productoInventRepository.GetProductDetails(id);
            productcollectionmodel.ProductImage = azureBlobStorage.DownloadFromBlob(productcollectionmodel.FileName).Result;
            return View("ProductDetails",productcollectionmodel);
        }

        // GET: ProductInvent/Create
        public ActionResult Create()
        {
            return View("AddProduct");
        }

        // POST: ProductInvent/Create
        [HttpPost]        
        public ActionResult AddProduct(ProductCollectionModel collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];
                    byte[] buffer = new byte[file.Length];
                    file.OpenReadStream().Read(buffer, 0, (int)file.Length);
                    collection.FileName = file.FileName;
                    collection.ProductImage = buffer;
                    azureBlobStorage.UploadToBlobAsync(collection);
                }
                else
                {
                    collection.FileName = "NoImage.png";
                }
                
               
                productoInventRepository.AddNewProduct(collection);
                sendEmailLogic.SendMail(collection);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: ProductInvent/Edit/5
        public ActionResult Edit(int id)
        {
            var productcollectionmodel=productoInventRepository.GetProductDetails(id);
            productcollectionmodel.ProductImage = azureBlobStorage.DownloadFromBlob(productcollectionmodel.FileName).Result;
            return View("EditProduct", productcollectionmodel);
        }

        // POST: ProductInvent/Edit/5
        [HttpPost]        
        public ActionResult EditProduct(ProductCollectionModel collection)
        {
            try
            {
                
                
                if (Request.Form.Files.Count>0)
                {
                    var file = Request.Form.Files[0];
                    byte[] buffer = new byte[file.Length];
                    file.OpenReadStream().Read(buffer, 0, (int)file.Length);
                    collection.FileName = file.FileName;
                    collection.ProductImage = buffer;
                    azureBlobStorage.UploadToBlobAsync(collection);
                }
                productoInventRepository.EditProduct(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductInvent/Delete/5
        public ActionResult Delete(int id)
        {
            var productcollectionmodel = productoInventRepository.GetProductDetails(id);
            productcollectionmodel.ProductImage = azureBlobStorage.DownloadFromBlob(productcollectionmodel.FileName).Result;
            return View("DeleteProduct", productcollectionmodel);
        }

        
        [HttpPost]        
        public ActionResult DeleteProduct(int id, ProductCollectionModel collection)
        {
            try
            {                
                productoInventRepository.DeleteProduct(collection.ProductId);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }
    }
}