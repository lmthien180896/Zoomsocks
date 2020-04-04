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

        public ActionResult Details(string alias)
        {            
            var product = productService.GetByAlias(alias);                        

            return PartialView("_Details", Mapper.Map<ProductViewModel>(product));
        }
    }
}