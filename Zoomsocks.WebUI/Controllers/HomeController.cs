using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zoomsocks.Service;
using Zoomsocks.WebUI.ViewModels;

namespace Zoomsocks.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IProductCategoryService productCategoryService;

        public HomeController(IProductCategoryService productCategoryService)
        {
            this.productCategoryService = productCategoryService;
        }

        public ActionResult Home()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            var productCategories = productCategoryService.GetAll();

            return PartialView("_Menu", Mapper.Map<IEnumerable<ProductCategoryViewModel>>(productCategories));
        }

        public ActionResult AboutUs()
        {
            return View();
        }
    }
}