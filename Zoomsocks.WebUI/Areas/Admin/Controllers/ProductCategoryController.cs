using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

        [HttpGet]
        public async Task<ActionResult> Create(CancellationToken cancellationToken)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductCategoryViewModel viewModel, CancellationToken cancellationToken)
        {
            var productCategory = Mapper.Map<ProductCategory>(viewModel);
            productCategoryService.Add(productCategory);
            productCategoryService.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<string> GetAlias(string name, CancellationToken cancellationToken)
        {
            return StringHelper.ToUnsignString(name);
        }

        [HttpGet]
        public ActionResult List(CancellationToken cancellationToken)
        {
            var productCategories = productCategoryService.GetAll();
            var viewModel = Mapper.Map<IEnumerable<ProductCategoryViewModel>>(productCategories);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Delete(Guid productCategoryId, string name)
        {
            return PartialView("_ConfirmDeleteModal", new ProductCategoryViewModel
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

            return PartialView("_EditModal", viewModel);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategoryViewModel viewModel)
        {
            var productCategory = Mapper.Map<ProductCategory>(viewModel);

            productCategoryService.Update(productCategory);
            productCategoryService.SaveChanges();

            return RedirectToAction("List");
        }
    }
}