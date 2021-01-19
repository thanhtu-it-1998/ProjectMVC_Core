using System;
using eShopSolution.Data.EF;
using System.Collections.Generic;
using eShopSolution.Utilities.Exceptions;
using eShopSolution.Data.Entities;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using eShopSolution.ViewModel.Catalog.Product.Manage;
using eShopSolution.ViewModel.Catalog.Product;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using eShopSolution.Application.Common;
using eShopSolution.ViewModel.Catalog.Product.Public;
using eShopSolution.ViewModel.Catalog.Common;

namespace eShopSolution.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        public readonly EShopDbContext context;
        public readonly IStorageService storageService;
        public ManageProductService(EShopDbContext _context, IStorageService _storageService)
        {
            context = _context;
            storageService = _storageService;
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
            if (request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption="Thumbnail image",
                        DateCreate = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath=await this.SaveFile(request.ThumbnailImage),
                        IsDefault= true,
                        SortOrder=1
                    }
                };
            }

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

            var images = context.ProductImages.Where(i => i.ProductId == productId);

            foreach (var i in images)
            {
                await storageService.DeleteFileAsync(i.ImagePath);
            }
            context.Products.Remove(product);

            return await context.SaveChangesAsync();
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await context.Products.FindAsync(request.Id);

            var productTranslations = await context.ProductTranslations
                .FirstOrDefaultAsync(x => x.ProductId == request.Id && x.LanguageId == request.LanguageId);

            if (product == null || productTranslations == null)
                throw new EShopException($"Can't find a product with id:{request.Id}");


            productTranslations.Name = request.Name;
            productTranslations.SeoAlias = request.SeoAlias;
            productTranslations.SeoDescription = request.SeoDescription;
            productTranslations.SeoTitle = request.SeoTitle;
            productTranslations.Description = request.Description;
            productTranslations.Details = request.Details;
            if (request.ThumbnailImage != null)
            {
                var ThumbnailImage = await context.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.ProductId == request.Id);

                if (ThumbnailImage != null)
                {
                    ThumbnailImage.FileSize = request.ThumbnailImage.Length;
                    ThumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                    context.ProductImages.Update(ThumbnailImage);
                }
            }
            return await context.SaveChangesAsync();

        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await context.Products.FindAsync(productId);
            if (product == null)
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


        public async Task<PageResult<ProductViewModel>> GetAllPaging(GetProductPagingRequestManage request)
        {
            var query = from p in context.Products
                        join pt in context.ProductTranslations on p.Id equals pt.ProductId
                        join pc in context.ProductCategorys on p.Id equals pc.ProductId
                        join c in context.Categories on p.Id equals c.Id
                        where (pt.Name.Contains(request.Keyword))
                        select new { p, pt, pc };
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


        public async Task AddViewCount(int productId)
        {
            var product = await context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await context.SaveChangesAsync();
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<bool> AddImage(int productId, FormFile file)
        {
            if (file == null) throw new EShopException($"File image null");
            var productImage = new ProductImage()
            {
                ProductId = productId,
                Caption = "Thumbnail image",
                DateCreate = DateTime.Now,
                FileSize = file.Length,
                ImagePath = await this.SaveFile(file),
                IsDefault = true,
                SortOrder = 1
            };
            context.ProductImages.Add(productImage);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RomoveImage(int imageId)
        {
            var image = await context.ProductImages.FindAsync(imageId);
            if (image != null)
            {
                throw new EShopException($"Can not find image with id:{imageId}");
            }
            await storageService.DeleteFileAsync(image.ImagePath);
            context.ProductImages.Remove(image);
            return await context.SaveChangesAsync() > 0;

        }

        public async Task<bool> UpdateImage(int imageId, string caption, bool isDefault)
        {
            var image = await context.ProductImages.FindAsync(imageId);

            if (image != null)
                throw new EShopException($"Can not find image with id:{imageId}");
            image.Caption = caption;
            image.IsDefault = isDefault;

            return await context.SaveChangesAsync() > 0;
        }

        public  async Task<List<ProductImageViewModel>> GetListImage(int productId)
        {
            var images =  await context.ProductImages.Where(p => p.ProductId == productId)
                .Select(p=> new ProductImageViewModel()
                {
                    File = p.FileSize,
                    FilePath=p.ImagePath,
                    Id= p.Id,
                    IsDefault=p.IsDefault,
                }).ToListAsync();
            
            return images;

        }
    }
}
