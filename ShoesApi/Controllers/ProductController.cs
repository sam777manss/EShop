using Microsoft.AspNetCore.Mvc;
using ShoesApi.DbContextFile.DBFiles;
using ShoesApi.Interfaces;
using ShoesApi.Models;
using ShoesApi.Models.ProductModel;

namespace ShoesApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct product;
        public ProductController(IProduct product)
        {
            this.product = product;
        }
        #region fetch category related products
        [HttpGet]
        [Route("Categories")]
        public async Task<IActionResult> Categories(string category)
        {
            List<AddProductTable> adminIndexes = new List<AddProductTable>();
            adminIndexes = await product.Categories(category);
            return Ok(adminIndexes);
        }
        #endregion

        #region fetch product info by id
        [HttpGet]
        [Route("InfoById")]
        public async Task<IActionResult> InfoById(string ProductId)
        {
            ProductInfo ProductInfo = new ProductInfo();
            ProductInfo = await product.InfoById(ProductId);
            return Ok(ProductInfo);
        }
        #endregion

        #region add new product
        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm] AddProduct addProductdata)
        {
            if (ModelState.IsValid)
            {
                IActionResult result = await product.AddProduct(addProductdata);
                // Return a response indicating success
                return new StatusCodeResult(200);
            }
            return BadRequest();
        }
        #endregion
        [HttpPost]
        [Route("ProductDetail")]
        public async Task<IActionResult> ProductDetail(UserCart cart)
        {
            IActionResult result = await product.ProductDetail(cart);
            return Ok();
        }

        #region fetch products in user card
        [HttpGet]
        [Route("UserCartDetails")]
        public async Task<IActionResult> UserCartDetails(string Uid)
        {
            UserCardModel userCartDetails = new UserCardModel();
            return Ok(await product.UserCartDetails(Uid));
        }
        #endregion

        #region DeleteProduct
        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(string UserCartTableId)
        {
            bool flag = await product.DeleteProduct(UserCartTableId);
            return Ok(true);
        }
        #endregion
    }
}
