using eShopSolution.Application.Catalog.Dtos;
using eShopSolution.Application.Catalog.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        public PageViewModel<ProductViewModel> GetAllByCategory(int categoryId, int pageIndex, int pageSize);
    }
}
