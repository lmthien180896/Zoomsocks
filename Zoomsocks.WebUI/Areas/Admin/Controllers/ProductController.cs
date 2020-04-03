using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zoomsocks.Common;
using Zoomsocks.Model.Models;
using Zoomsocks.Service;
using Zoomsocks.WebUI.Shared.Mvc;
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

            LoadCategories(viewModel);
            viewModel.IsCreateWizard = true;

            return View("Create", viewModel);            
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel viewModel)
        {
            LoadCategories(viewModel);

            if (!ModelState.IsValid)
            {
                this.PrepareErrorMessage("Cannot create this product.");
                return View("Create", viewModel);
            }

            viewModel.MoreImages = JsonConvert.SerializeObject(viewModel.MoreImagesList);

            var product = Mapper.Map<Product>(viewModel);
            productService.Add(product);
            productService.SaveChanges();

            this.PrepareSuccessMessage($"{product.Name} has been created.");

            return View("List");
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
            foreach (var product in listProduct)
            {
                product.Category = productCategoryService.GetById(product.ProductCategoryId).Name;
            }

            return Json(new
            {
                list = listProduct.OrderBy(x => x.Category)
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Delete(Guid productId, string name)
        {
            return PartialView("_Delete", new ProductViewModel
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

            return View("List");
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var product = productService.GetById(id);
            var viewModel = Mapper.Map<ProductViewModel>(product);

            LoadCategories(viewModel);
            viewModel.MoreImagesList = JsonConvert.DeserializeObject<string[]>(viewModel.MoreImages);
            viewModel.IsCreateWizard = false;

            return View("Create", viewModel);
        }

        private void LoadCategories(ProductViewModel viewModel)
        {
            var productCategories = productCategoryService.GetAll();

            viewModel.Categories = productCategories.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToArray();
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel viewModel)
        {
            LoadCategories(viewModel);

            if (!ModelState.IsValid)
            {
                this.PrepareErrorMessage("Cannot create this product.");
                return View("Create", viewModel);
            }

            viewModel.MoreImages = JsonConvert.SerializeObject(viewModel.MoreImagesList);

            var product = Mapper.Map<Product>(viewModel);
            productService.Update(product);
            productService.SaveChanges();

            this.PrepareErrorMessage($"{product.Name} has been updated successfully.");

            return View("List");
        }
    }
}