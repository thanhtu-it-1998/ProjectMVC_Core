using eShopSolution.Application.Catalog.Products;
using eShopSolution.ViewModel.Catalog.Product.Manage;
using eShopSolution.ViewModel.Catalog.Product.Public;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IPublicProductService _publicProductService;
        public readonly IManageProductService _manageProcductService;
        public ProductController(IPublicProductService publicProductService, IManageProductService manageProductService)
        {
            _publicProductService = publicProductService;
            _manageProcductService = manageProductService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _publicProductService.GetAll();
            return Ok(products);
        }
        [HttpGet("public-paging")]
        public async Task<IActionResult> Get([FromQuery] GetProductPagingRequestPublic request)
        {
            var products = await _publicProductService.GetAllByCategoryID(request);
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _manageProcductService.GetById(id);
            if (product == null) return BadRequest("Canot find product");
            return Ok(product);
        }
        [HttpGet()]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateRequest request)
        {
            var productId = await _manageProcductService.Create(request);
            if (productId == 0) return BadRequest();
            var product = await _manageProcductService.GetById(productId);
            return CreatedAtAction(nameof(GetById), new { id = productId }, product);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductCreateRequest request)
        {
            var affectedResult = await _manageProcductService.Create(request);
            if (affectedResult == 0) return BadRequest();
            return Ok();
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var affectedResult = await _manageProcductService.Delete(id);
            if (affectedResult == 0) return BadRequest();
            return Ok();
        }
        [HttpPut("price/{id}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice([FromQuery] int id, decimal newPrice)
        {
            var isSuccessFull = await _manageProcductService.UpdatePrice(id, newPrice);
            if (!isSuccessFull) return BadRequest();
            return Ok();
        }
        [HttpDelete("id")] 
        public async Task<IActionResult> DeleteImage(int id)
        {
            var affectedReuslt = await _manageProcductService.RomoveImage(id);
            if (affectedReuslt) return Ok();
            return BadRequest();
        }
        [HttpPut("image/{id}/{caption}/{isDefault}")] 
        public async Task<IActionResult> UpdateImage([FromForm]int id,string caption,bool isDefault)
        {
            var affectedReuslt = await _manageProcductService.UpdateImage(id,caption,isDefault);
            if (affectedReuslt) return Ok();
            return BadRequest();
        }
        [HttpGet("id")] 
        public async Task<IActionResult> GetListImage([FromBody] int id)
        {
            var affectedReuslt = await _manageProcductService.GetListImage(id);
            return Ok(affectedReuslt);
        }
    }
}
