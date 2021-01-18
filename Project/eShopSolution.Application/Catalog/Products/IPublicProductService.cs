 using eShopSolution.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using eShopSolution.Application.Catalog.Products.Dtos.Public;
using eShopSolution.Application.Catalog.Products.Dtos.Manage;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        Task<PageResult<ProductViewModel>>  GetAllByCategory (GetProductPagingRequestPublic request);
    }
}
