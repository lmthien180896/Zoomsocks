using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoomsocks.Data.Infrastructure;
using Zoomsocks.Data.Repositories;
using Zoomsocks.Model.Models;

namespace Zoomsocks.Service
{
    public interface IProductCategoryService
    {
        void Add(ProductCategory productCategory);

        void Delete(Guid id);

        void Update(ProductCategory productCategory);

        IEnumerable<ProductCategory> GetAll();

        IEnumerable<ProductCategory> GetAllPaging(int page, int pageSize, out int totalRow);

        ProductCategory GetById(Guid id);

        ProductCategory GetByAlias(string alias);

        void SaveChanges();
    }

    public class ProductCategoryService : IProductCategoryService
    {
        private IProductCategoryRepository productCategoryRepository;
        private IUnitOfWork unitOfWork;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IUnitOfWork unitOfWork)
        {
            this.productCategoryRepository = productCategoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public void Add(ProductCategory productCategory)
        {
            productCategoryRepository.Add(productCategory);
        }

        public void Delete(Guid id)
        {
            productCategoryRepository.Delete(id);
            
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return productCategoryRepository.GetAll().OrderBy(p => p.DisplayOrder);
        }

        public IEnumerable<ProductCategory> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return productCategoryRepository.GetMultiPaging(out totalRow, page, pageSize);
        }

        public ProductCategory GetById(Guid id)
        {
            return productCategoryRepository.GetSingleById(id);
        }

        public void Update(ProductCategory productCategory)
        {
            productCategoryRepository.Update(productCategory);
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }

        public ProductCategory GetByAlias(string alias)
        {
            return productCategoryRepository.GetSingleByCondition(x => x.Alias == alias);
        }
    }
}
