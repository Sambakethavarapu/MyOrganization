using Microsoft.AspNetCore.Http.HttpResults;

namespace MyOrganization.DataModel
{
    public class ProductDetails
    {
        public int ProductId { get; set; }
        public string? ProductType { get; set; }
        public string? ProductName { get; set; }
        public double PricePerItem { get; set; }
        public bool InOffer { get; set; }
        public double OfferPercentage { get; set; }
        public double ProductItems { get; set; }
        public DateTime DateCreated { get; set; }
        public string? CreatedBy { get; set; }
        public bool IsExists { get; set; }
    }
}
