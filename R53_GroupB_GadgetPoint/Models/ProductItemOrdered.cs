namespace R53_GroubB_GadgetPoint.Models
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
        }

        public ProductItemOrdered(int productItemId, string productName, string pictureUrl)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public int ProductItemId { get; set; }
        public string? ProductName { get; set; }
        public string? PictureUrl { get; set; }
    }
}
