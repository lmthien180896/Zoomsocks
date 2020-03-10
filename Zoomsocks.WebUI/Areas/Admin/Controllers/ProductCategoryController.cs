using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
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
        public async Task<ActionResult> List(CancellationToken cancellationToken)
        {
            var productCategories = await GetAllProductCategories(cancellationToken);

            var viewModel = Mapper.Map<IEnumerable<ProductCategoryViewModel>>(productCategories);

            return View(viewModel);
        }

        private async Task<IEnumerable<ProductCategory>> GetAllProductCategories(CancellationToken cancellationToken)
        {
            return productCategoryService.GetAll();
        }
    }
}