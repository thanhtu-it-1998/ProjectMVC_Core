using eShopSolution.Application.Dtos;
using eShopSolution.Application.Catalog.Products.Dtos.Manage;
using System;
using eShopSolution.Data.EF;
using System.Collections.Generic;
using eShopSolution.Utilities.Exceptions;
using eShopSolution.Data.Entities;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eShopSolution.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        public readonly EShopDbContext context;
        public ManageProductService(EShopDbContext _context)
        {
            context = _context;
        }
        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name=request.Name,
                        Description=request.Description,
                        Details= request.Details,
                        LanguageId=request.LanguageId,
                        SeoAlias = request.SeoAlias,
                        SeoDescription= request.SeoDescription,
                        SeoTitle= request.SeoTitle

                    }
                }

            };
            context.Products.Add(product);
            return await context.SaveChangesAsync();
        }

        public async Task<int> Delete(int productId)
        {
            var product = await context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new EShopException($"Can't find a product:{productId}");
            }
            context.Products.Remove(product);

            return await context.SaveChangesAsync();
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await context.Products.FindAsync(request.Id);
            
            var productTranslations = await context.ProductTranslations
                .FirstOrDefaultAsync(x=>x.ProductId == request.Id && x.LanguageId == request.LanguageId);
            
            if (product == null || productTranslations == null)
                throw new EShopException($"Can't find a product with id:{request.Id}");


            productTranslations.Name = request.Name;
            productTranslations.SeoAlias= request.SeoAlias;
            productTranslations.SeoDescription= request.SeoDescription;
            productTranslations.SeoTitle= request.SeoTitle;
            productTranslations.Description= request.Description;
            productTranslations.Details= request.Details;
            return await context.SaveChangesAsync();

        }

        public  async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await context.Products.FindAsync(productId);
            if (product == null )
                throw new EShopException($"Can't find a product with id:{productId}");
            product.Price = newPrice;
            return await context.SaveChangesAsync() > 0;

        }
        public async Task<bool> UpdateStock(int productId, int addedQuantity)
        {
            var product = await context.Products.FindAsync(productId);
            if (product == null)
                throw new EShopException($"Can't find a product with id:{productId}");
            product.Stock += addedQuantity;
            return await context.SaveChangesAsync() > 0;

        }


        public async Task<PageResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request)
        {
            var query = from p in context.Products
                        join pt in context.ProductTranslations on p.Id equals pt.ProductId
                        join pc in context.ProductCategorys on p.Id equals pc.ProductId
                        join c in context.Categories on p.Id equals c.Id
                        where (pt.Name.Contains(request.Keyword))
                        select new { p, pt,pc };
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.pt.Name.Contains(request.Keyword));
            }
            if (request.CategoryId.Count > 0)
            {
                query = query.Where(x => request.CategoryId.Contains(x.pc.CategoryId));
            }

            int totalRow = await query.CountAsync();

            var data = query.Skip((request.PageIndex - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .Select(x=>new ProductViewModel() {
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
                            }).ToList()   ;

            var pageResult = new PageResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data,

            };
            return pageResult;
        }


        public async Task AddViewCount(int productId)
        {
            var product = await context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await context.SaveChangesAsync();
        }

      
    }
}
