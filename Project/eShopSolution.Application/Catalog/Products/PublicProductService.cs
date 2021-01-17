using eShopSolution.Application.Dtos;
using eShopSolution.Application.Catalog.Products.Dtos.Manage;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Application.Catalog.Products
{
    class PublicProductService : IPublicProductService
    {
        public PageResult<ProductViewModel> GetAllByCategory(GetProductPagingRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
