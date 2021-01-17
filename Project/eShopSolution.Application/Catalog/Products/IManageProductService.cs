using eShopSolution.Application.Catalog.Dtos;
using eShopSolution.Application.Catalog.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IManageProductService
    {   

        int Create(ProductCreateRequest request);
        int Edit(ProductEditRequest request);
        int Delete(int productId);
        List<ProductViewModel> GetAll();
        PageViewModel<ProductViewModel> GetAllPaging(string keyword, int pageIndex,int pageSize);

    }
}
