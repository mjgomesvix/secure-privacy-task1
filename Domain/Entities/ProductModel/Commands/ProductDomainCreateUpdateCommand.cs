namespace DomainLayer.Entities.ProductModel.Commands
{
    public class ProductDomainCreateUpdateCommand
    {
        public string Name { get; set; }
        public string Unity { get; set; }
        public decimal Price { get; set; }
    }
}
