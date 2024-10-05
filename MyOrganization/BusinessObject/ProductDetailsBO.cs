using MyOrganization.DataAccessObject;
using MyOrganization.DataModel;

namespace MyOrganization.BusinessObject
{
    public class ProductDetailsBO
    {
        public ProductDetailsBO() { }
        SqlUserDAO dao = new SqlUserDAO();
        public async Task<List<ProductDetails>> GetAllProductDetails()
        {
            try
            {
                List<ProductDetails> prodDetails = new List<ProductDetails>();
                return prodDetails = await dao.GetAllProductDetails();

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<ProductDetails>> SaveProductDetails(ProductDetails productDetails)
        {
            try
            {
                List<ProductDetails> productsDetails = new List<ProductDetails>();
                return productsDetails = await dao.SaveProductDetails(productDetails);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
