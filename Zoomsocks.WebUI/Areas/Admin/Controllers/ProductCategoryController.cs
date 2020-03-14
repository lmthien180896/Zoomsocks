using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Mvc;
using Zoomsocks.Common;
using Zoomsocks.Model.Models;
using Zoomsocks.Service;
using Zoomsocks.WebUI.ViewModels;

namespace Zoomsocks.WebUI.Areas.Admin.Controllers
{
    public class ProductCategoryController : Controller
    {
        private IProductCategoryService productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            this.productCategoryService = productCategoryService;
        }

        public ActionResult List()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create(CancellationToken cancellationToken)
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public JsonResult Create(ProductCategoryViewModel viewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false });
            }
            var productCategory = Mapper.Map<ProductCategory>(viewModel);
            productCategoryService.Add(productCategory);
            productCategoryService.SaveChanges();

            return Json(new { success = true, name = productCategory.Name });
        }

        [HttpGet]
        public string GetAlias(string name, CancellationToken cancellationToken)
        {
            return StringHelper.ToUnsignString(name);
        }

        [HttpGet]
        public JsonResult LoadAll(CancellationToken cancellationToken)
        {
            var productCategories = productCategoryService.GetAll();
            var listProductCategory = Mapper.Map<IEnumerable<ProductCategoryViewModel>>(productCategories);

            return Json(new
            {
                list = listProductCategory
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Delete(Guid productCategoryId, string name)
        {
            return PartialView("_Delete", new ProductCategoryViewModel
            {
                Id = productCategoryId,
                Name = name
            });
        }

        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            productCategoryService.Delete(id);
            productCategoryService.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var productCategory = productCategoryService.GetById(id);
            var viewModel = Mapper.Map<ProductCategoryViewModel>(productCategory);

            return PartialView("_Edit", viewModel);
        }

        [HttpPost]
        public JsonResult Edit(ProductCategoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, JsonRequestBehavior.AllowGet });
            }

            var productCategory = Mapper.Map<ProductCategory>(viewModel);

            productCategoryService.Update(productCategory);
            productCategoryService.SaveChanges();

            return Json(new { success = true, data = productCategory, JsonRequestBehavior.AllowGet });
        }
    }
}