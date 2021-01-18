using eShopSolution.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Application.Catalog.Products.Dtos.Manage
{
   public class GetProductPagingRequestManage:PagingRequestBase
    {
        public string Keyword { get; set; }
        public List<int> CategoryId { get; set; }
    }

}
