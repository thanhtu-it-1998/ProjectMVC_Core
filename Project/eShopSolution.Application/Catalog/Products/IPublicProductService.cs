
using eShopSolution.ViewModel.Catalog.Common;
using eShopSolution.ViewModel.Catalog.Product;
using eShopSolution.ViewModel.Catalog.Product.Public;
using System.Threading.Tasks;


namespace eShopSolution.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        Task<PageResult<ProductViewModel>>  GetAllByCategory (GetProductPagingRequestPublic request);
    }
}
