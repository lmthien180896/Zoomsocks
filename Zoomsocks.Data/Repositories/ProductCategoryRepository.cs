using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zoomsocks.Data.Infrastructure;
using Zoomsocks.Model.Models;

namespace Zoomsocks.Data.Repositories
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {

    }

    public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
