using eShopSolution.Application.Catalog.Dtos;
using eShopSolution.Application.Catalog.Products.Dtos;
using System;
using eShopSolution.Data.EF;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        public ManageProductService(EShopDbContext context)
        {

        }
        public int Create(ProductCreateRequest request)
        {
            throw new NotImplementedException();
        }

        public int Delete(int productId)
        {
            throw new NotImplementedException();
        }

        public int Edit(ProductEditRequest request)
        {
            throw new NotImplementedException();
        }

        public List<ProductViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public PageViewModel<ProductViewModel> GetAllPaging(string keyword, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
