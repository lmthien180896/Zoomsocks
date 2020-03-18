using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zoomsocks.Model.Models;
using Zoomsocks.Service;
using Zoomsocks.WebUI.ViewModels;

namespace Zoomsocks.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private IProductCategoryService productCategoryService;
        private IProductService productService;

        public CategoryController(IProductCategoryService productCategoryService, IProductService productService)
        {
            this.productCategoryService = productCategoryService;
            this.productService = productService;
        }

        public ActionResult ProductsByCategory(string alias)
        {
            var category = productCategoryService.GetByAlias(alias);

            var products = productService.GetByCategory(category.Id);   

            return View(new DisplayCategoryViewModel
            {
                Category = Mapper.Map<ProductCategoryViewModel>(category),
                Products = Mapper.Map<IEnumerable<ProductViewModel>>(products)
            });
        }
    }
}