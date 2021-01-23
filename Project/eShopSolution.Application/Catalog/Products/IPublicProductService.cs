
using eShopSolution.ViewModel.Catalog.Common;
using eShopSolution.ViewModel.Catalog.Product;
using eShopSolution.ViewModel.Catalog.Product.Public;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace eShopSolution.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        Task<PageResult<ProductViewModel>>  GetAllByCategoryID (GetProductPagingRequestPublic request);
        Task<List<ProductViewModel>> GetAll();
    }
}
