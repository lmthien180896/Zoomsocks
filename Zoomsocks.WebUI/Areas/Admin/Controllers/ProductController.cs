using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zoomsocks.Common;
using Zoomsocks.Model.Models;
using Zoomsocks.Service;
using Zoomsocks.WebUI.ViewModels;

namespace Zoomsocks.WebUI.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private IProductCategoryService productCategoryService;
        private IProductService productService;

        public ProductController(IProductCategoryService productCategoryService, IProductService productService)
        {
            this.productCategoryService = productCategoryService;
            this.productService = productService;
        }

        public ActionResult List()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new ProductViewModel();

            LoadCategories();

            return PartialView("_Create", viewModel);

            void LoadCategories()
            {
                var productCategories = productCategoryService.GetAll();
                
                viewModel.Categories = productCategories.Select(p => new SelectListItem() { Text = p.Name, Value = p.Name}).ToArray();
            }
        }

        [HttpPost]
        public JsonResult Create(ProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false });
            }
            var productCategory = Mapper.Map<Product>(viewModel);

            productService.Add(productCategory);
            productService.SaveChanges();

            return Json(new { success = true, name = productCategory.Name });
        }

        [HttpGet]
        public string GetAlias(string name)
        {
            return StringHelper.ToUnsignString(name);
        }

        [HttpGet]
        public JsonResult LoadAll()
        {
            var products = productService.GetAll();
            var listProduct = Mapper.Map<IEnumerable<ProductViewModel>>(products);

            return Json(new
            {
                list = products
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Delete(Guid productId, string name)
        {
            return PartialView("_Delete", new ProductCategoryViewModel
            {
                Id = productId,
                Name = name
            });
        }

        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            productService.Delete(id);
            productService.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var product = productService.GetById(id);
            var viewModel = Mapper.Map<ProductViewModel>(product);

            return PartialView("_Edit", viewModel);
        }

        [HttpPost]
        public JsonResult Edit(ProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, JsonRequestBehavior.AllowGet });
            }

            var product = Mapper.Map<Product>(viewModel);

            productService.Update(product);
            productService.SaveChanges();

            return Json(new { success = true, data = product, JsonRequestBehavior.AllowGet });
        }
    }
}