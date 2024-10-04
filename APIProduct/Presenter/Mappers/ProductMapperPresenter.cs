using APIProduct.Domain.Entities;
using APIProduct.Presenter.Entities;

namespace APIProduct.Presenter.Mappers
{
    public class ProductMapperPresenter
    {
        public ProductPresenter fromDomainToPresenter(ProductDomain productDomain)
        {
            return new ProductPresenter
            {
                ProductId = productDomain.ProductId,
                ProductName = productDomain.ProductName,
                Price = productDomain.Price,
                CreatedBy = productDomain.CreatedBy,
                CreatedAt = productDomain.CreatedAt
            };
        }

        public ProductDomain fromPresenterToDomain(ProductPresenter productPresenter)
        {
            return new ProductDomain
            {
                ProductId = productPresenter.ProductId,
                ProductName = productPresenter.ProductName,
                Price = productPresenter.Price,
                CreatedBy = productPresenter.CreatedBy,
                CreatedAt = productPresenter.CreatedAt
            };
        }
    }
}
