using DomainLayer.Entities.ProductModel.Commands;
using DomainLayer.Resources;
using Support.ExceptionsManagement;

namespace DomainLayer.Entities.ProductModel
{
    public static class ProductFactory
    {
        public static Product Create(this ProductDomainCreateUpdateCommand productDomainCreateUpdateCommand)
        {
            productDomainCreateUpdateCommand.CheckIntegrity();

            return new Product()
            {
                Name = productDomainCreateUpdateCommand.Name,
                Unity = productDomainCreateUpdateCommand.Unity,
                Price = productDomainCreateUpdateCommand.Price
            };
        }

        public static void Update(this Product product, ProductDomainCreateUpdateCommand productDomainCreateUpdateCommand)
        {
            productDomainCreateUpdateCommand.CheckIntegrity();

            product.Name = productDomainCreateUpdateCommand.Name;
            product.Unity = productDomainCreateUpdateCommand.Unity;
            product.Price = productDomainCreateUpdateCommand.Price;
        }

        private static void CheckIntegrity(this ProductDomainCreateUpdateCommand productDomainCreateUpdateCommand)
        {
            var integrityCheckup = new IntegrityCheckup();
            integrityCheckup.CheckRequired(productDomainCreateUpdateCommand.Name, string.Format(DomainMessages.Required, "Name"));
            integrityCheckup.CheckRequired(productDomainCreateUpdateCommand.Unity, string.Format(DomainMessages.Required, "Unity"));
            integrityCheckup.CheckRequired(productDomainCreateUpdateCommand.Price, string.Format(DomainMessages.Required, "Price"));
            integrityCheckup.ThrowExceptions();
        }
    }
}