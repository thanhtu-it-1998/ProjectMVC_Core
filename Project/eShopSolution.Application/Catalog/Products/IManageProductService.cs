using eShopSolution.ViewModel.Catalog.Common;
using eShopSolution.ViewModel.Catalog.Product;
using eShopSolution.ViewModel.Catalog.Product.Manage;
using eShopSolution.ViewModel.Catalog.Product.Public;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IManageProductService
    {

        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(int productId);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task AddViewCount(int productId);
        Task<bool> UpdateStock(int productId, int addedQuantity);
        Task<bool> AddImage(int productId, FormFile file);
        Task<bool> RomoveImage(int imageId);
        Task<bool> UpdateImage(int imageId,string caption,bool isDefault);
        Task<PageResult<ProductViewModel>> GetAllPaging(GetProductPagingRequestManage request);
        Task<List<ProductImageViewModel>> GetListImage(int productId);

    }
}
