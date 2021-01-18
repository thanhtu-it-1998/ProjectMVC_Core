
using eShopSolution.ViewModel.Catalog.Common;
using System.Collections.Generic;

namespace eShopSolution.ViewModel.Catalog.Product.Manage
{
   public class GetProductPagingRequestManage:PagingRequestBase
    {
        public string Keyword { get; set; }
        public List<int> CategoryId { get; set; }
    }

}
