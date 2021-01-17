using eShopSolution.Application.Dtos;
using eShopSolution.Application.Catalog.Products.Dtos.Manage;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        public PageResult<ProductViewModel> GetAllByCategory(GetProductPagingRequest request);
    }
}
