

using eShopSolution.ViewModel.Catalog.Common;

namespace eShopSolution.ViewModel.Catalog.Product.Public
{
   public class GetProductPagingRequestPublic :PagingRequestBase
    {
        public int? CategoryId { get; set; }
    }
}
