using AutoMapper;
using Newtonsoft.Json;
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

            Mapper.CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.MoreImagesList, opt => opt.ResolveUsing(src => GetMoreImagesList(src.MoreImages)));           

            Mapper.CreateMap<ProductViewModel, Product>();

            string[] GetMoreImagesList(string moreImages)
            {
                return JsonConvert.DeserializeObject<string[]>(moreImages);
            }
        }        
    }
}