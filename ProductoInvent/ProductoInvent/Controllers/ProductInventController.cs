using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductoInvent.Models;

namespace ProductoInvent
{
    public class ProductInventController : Controller
    {
        
        private ProductoInventRepository productoInventRepository;

        public ProductInventController()
        {
            productoInventRepository = new ProductoInventRepository();
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
            return View("ProductDetails",productcollectionmodel);
        }

        // GET: ProductInvent/Create
        public ActionResult Create()
        {
            return View("AddProduct");
        }

        // POST: ProductInvent/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(ProductCollectionModel collection)
        {
            try
            {
                // TODO: Add insert logic here
                productoInventRepository.AddNewProduct(collection);
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
            return View("EditProduct", productcollectionmodel);
        }

        // POST: ProductInvent/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductCollectionModel collection)
        {
            try
            {
                // TODO: Add update logic here

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
            return View("DeleteProduct", productcollectionmodel);
        }

        // POST: ProductInvent/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(int id, ProductCollectionModel collection)
        {
            try
            {
                // TODO: Add delete logic here
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