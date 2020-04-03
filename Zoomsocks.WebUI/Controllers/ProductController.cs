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
    public class ProductController : Controller
    {
        private IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public ActionResult QuickView(string productId)
        {
            var guidId = new Guid(productId);
            var product = productService.GetById(guidId);

            return PartialView("_QuickViewProduct", Mapper.Map<ProductViewModel>(product));
        }
    }
}