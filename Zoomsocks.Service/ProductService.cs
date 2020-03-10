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

        Product GetById(Guid id);

        void SaveChanges();
    }

    public class ProductService : IProductService
    {
        IProductRepository ProductRepository;
        IUnitOfWork unitOfWork;

        public ProductService(IProductRepository ProductRepository, IUnitOfWork unitOfWork)
        {
            this.ProductRepository = ProductRepository;
            this.unitOfWork = unitOfWork;
        }

        public void Add(Product Product)
        {
            ProductRepository.Add(Product);
        }

        public void Delete(Guid id)
        {
            ProductRepository.Delete(id);

        }

        public IEnumerable<Product> GetAll()
        {
            return ProductRepository.GetAll();
        }

        public IEnumerable<Product> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return ProductRepository.GetMultiPaging(out totalRow, page, pageSize);
        }

        public Product GetById(Guid id)
        {
            return ProductRepository.GetSingleById(id);
        }

        public void Update(Product Product)
        {
            ProductRepository.Update(Product);
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
    }
}
