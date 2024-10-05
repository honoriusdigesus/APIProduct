using APIProduct.Data.Context;
using APIProduct.Domain.Entities;
using APIProduct.Domain.Exceptions.Exception;
using APIProduct.Domain.Mappers;

namespace APIProduct.Domain.UseCases
{
    public class CreateProductUseCase
    {
        private readonly MyAppDbContext _context;
        private readonly ProductMapperDomain _productMapperDomain;
        private readonly Utlis.MyValidator _myValidator;

        public CreateProductUseCase(MyAppDbContext context, ProductMapperDomain productMapperDomain, Utlis.MyValidator myValidator)
        {
            _context = context;
            _productMapperDomain = productMapperDomain;
            _myValidator = myValidator;
        }

        public async Task<ProductDomain> Execute(ProductDomain productDomain)
        {
            //Validated fields of productDomain
            if (!_myValidator.IsString(productDomain.ProductName))
            {
                throw new ProductException("ProductName is required, please verify the information");
            }
            if (productDomain.Price<0 || productDomain.Price.Equals(""))
            {
                throw new ProductException("Price is required, please verify the information");
            }
            await Task.CompletedTask;
            Data.Models.Product product = _productMapperDomain.fromDomainToData(productDomain);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return _productMapperDomain.fromDataToDomain(product);
        }
    }
}
