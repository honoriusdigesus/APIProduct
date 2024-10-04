using APIProduct.Data.Models;
using APIProduct.Domain.Entities;

namespace APIProduct.Domain.Mappers
{
    public class ProductMapperDomain
    {
        public ProductDomain fromDataToDomain(Product product)
        {
            return new ProductDomain
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                CreatedBy = product.CreatedBy,
                CreatedAt = product.CreatedAt
            };
        }

        public Product fromDomainToData(ProductDomain productDomain)
        {
            return new Product
            {
                ProductId = productDomain.ProductId,
                ProductName = productDomain.ProductName,
                Price = productDomain.Price,
                CreatedBy = productDomain.CreatedBy,
                CreatedAt = productDomain.CreatedAt
            };
        }
    }
}
