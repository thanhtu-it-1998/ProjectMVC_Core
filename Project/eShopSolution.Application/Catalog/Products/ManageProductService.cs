using eShopSolution.Application.Dtos;
using eShopSolution.Application.Catalog.Products.Dtos.Manage;
using System;
using eShopSolution.Data.EF;
using System.Collections.Generic;
using System.Text;
using eShopSolution.Data.Entities;
using System.Threading.Tasks;

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
            if(product == null)
            {
                throw new NotImplementedException();
            }
            context.Products.Remove(product);

            await context.SaveChangesAsync();
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PageResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            throw new NotImplementedException();
        }

        public async Task AddViewCount(int productId)
        {
            var product = await context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await context.SaveChangesAsync();
        }

        public Task<bool> UpdateStock(int productId, int addedQuantity)
        {
            throw new NotImplementedException();
        }
    }
}
