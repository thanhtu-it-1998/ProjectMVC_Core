using System.Collections.Generic;
using System.Threading.Tasks;
using eShopSolution.Data.EF;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using eShopSolution.ViewModel.Catalog.Common;
using eShopSolution.ViewModel.Catalog.Product;
using eShopSolution.ViewModel.Catalog.Product.Manage;
using eShopSolution.ViewModel.Catalog.Product.Public;

namespace eShopSolution.Application.Catalog.Products
{
    class PublicProductService : IPublicProductService
    {
        public readonly EShopDbContext context;
        public PublicProductService(EShopDbContext _context)
        {
            context = _context;
        }

        public async Task<PageResult<ProductViewModel>> GetAllByCategory(GetProductPagingRequestPublic request)
        {
            var query = from p in context.Products
                        join pt in context.ProductTranslations on p.Id equals pt.ProductId
                        join pc in context.ProductCategorys on p.Id equals pc.ProductId
                        join c in context.Categories on p.Id equals c.Id
                        select new { p, pt, pc };
            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
            {
                query = query.Where(p => p.pc.CategoryId == request.CategoryId);
            }


            int totalRow = await query.CountAsync();

            var data = query.Skip((request.PageIndex - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .Select(x => new ProductViewModel()
                            {
                                Id = x.p.Id,
                                Name = x.pt.Name,
                                DateCreated = x.p.DateCreated,
                                Description = x.pt.Description,
                                Details = x.pt.Details,
                                LanguageId = x.pt.LanguageId,
                                OriginalPrice = x.p.OriginalPrice,
                                Price = x.p.Price,
                                SeoAlias = x.pt.SeoAlias,
                                SeoDescription = x.pt.SeoDescription,
                                SeoTitle = x.pt.SeoTitle,
                                Stock = x.p.Stock,
                                ViewCount = x.p.ViewCount
                            }).ToList();

            var pageResult = new PageResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data,

            };
            return pageResult;
        }
    }
}
