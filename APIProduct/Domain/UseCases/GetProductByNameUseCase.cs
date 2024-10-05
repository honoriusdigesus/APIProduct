using APIProduct.Data.Context;
using APIProduct.Domain.Entities;
using APIProduct.Domain.Exceptions.Exception;
using APIProduct.Domain.Mappers;
using Microsoft.EntityFrameworkCore;

namespace APIProduct.Domain.UseCases
{
    public class GetProductByNameUseCase
    {
        private readonly MyAppDbContext _context;
        private readonly ProductMapperDomain _productMapperDomain;
        private readonly Utlis.MyValidator _myValidator;

        public GetProductByNameUseCase(MyAppDbContext context, ProductMapperDomain productMapperDomain, Utlis.MyValidator myValidator)
        {
            _context = context;
            _productMapperDomain = productMapperDomain;
            _myValidator = myValidator;
        }

        public async Task<ProductDomain> Execute(string productName)
        {
            if (!_myValidator.IsString(productName))
            {
                throw new ProductException("ProductName is required, please verify the information");
            }
            Data.Models.Product? product = await _context.Products.FirstOrDefaultAsync(p => p.ProductName == productName);
            if (product == null)
            {
                throw new ProductException("Product not found");
            }
            return _productMapperDomain.fromDataToDomain(product);
        }
    }
}
