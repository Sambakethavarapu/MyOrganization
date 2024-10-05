using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOrganization.BusinessObject;
using MyOrganization.DataModel;

namespace MyOrganization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public ProductController()
        {
        }

        [Route("GetAllProductDetails")]
        [HttpGet]
        public async Task<List<ProductDetails>> GetAllProductDetails()
        {
            try
            {
                List<ProductDetails> productsDetails = new List<ProductDetails>();
                ProductDetailsBO productDetails = new ProductDetailsBO();
                productsDetails = await productDetails.GetAllProductDetails();
                if (productDetails != null) { return productsDetails; }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        [Route("SaveProductDetails")]
        [HttpPost]
        public async Task<List<ProductDetails>> SaveProductDetails([FromBody] ProductDetails productDetails)
        {
            try
            {
                List<ProductDetails> empDetails = new List<ProductDetails>();
                ProductDetailsBO userDetails = new ProductDetailsBO();
                return empDetails = await userDetails.SaveProductDetails(productDetails);

            }
            catch (Exception)
            {

                throw;
            }
            //return Ok(empDetails);
        }


    }
}
