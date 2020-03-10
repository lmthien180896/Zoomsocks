using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zoomsocks.Model.Models;
using Zoomsocks.WebUI.ViewModels;

namespace Zoomsocks.WebUI.MappingProfiles
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<ProductCategory, ProductCategoryViewModel>();
            Mapper.CreateMap<ProductCategoryViewModel, ProductCategory>();

            Mapper.CreateMap<Product, ProductViewModel>();
            Mapper.CreateMap<ProductViewModel, Product>();
        }
    }
}