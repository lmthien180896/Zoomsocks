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
    public interface IProductService
    {
        void Add(Product Product);

        void Delete(Guid id);

        void Update(Product Product);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAllPaging(int page, int pageSize, out int totalRow);

        IEnumerable<Product> GetByCategory(Guid id);

        Product GetById(Guid id);

        Product GetByAlias(string alias);

        bool DoesNameExist(string name, Guid categoryId);

        void SaveChanges();
    }

    public class ProductService : IProductService
    {
        IProductRepository productRepository;
        IUnitOfWork unitOfWork;

        public ProductService(IProductRepository ProductRepository, IUnitOfWork unitOfWork)
        {
            this.productRepository = ProductRepository;
            this.unitOfWork = unitOfWork;
        }

        public void Add(Product Product)
        {
            productRepository.Add(Product);
        }

        public void Delete(Guid id)
        {
            productRepository.Delete(id);

        }

        public IEnumerable<Product> GetAll()
        {
            return productRepository.GetAll();
        }

        public IEnumerable<Product> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return productRepository.GetMultiPaging(out totalRow, page, pageSize);
        }

        public Product GetById(Guid id)
        {
            return productRepository.GetSingleById(id);
        }

        public void Update(Product Product)
        {
            productRepository.Update(Product);
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<Product> GetByCategory(Guid id)
        {
            return productRepository.GetMulti(x => x.ProductCategoryId == id);
        }

        public Product GetByAlias(string alias)
        {
            return productRepository.GetSingleByCondition(x => x.Alias == alias);
        }

        public bool DoesNameExist(string name, Guid categoryId)
        {
            return productRepository.GetSingleByCondition(x => x.Name == name && x.ProductCategoryId == categoryId) != null;
        }
    }
}
