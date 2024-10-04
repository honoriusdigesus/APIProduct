using APIProduct.Data.Context;
using APIProduct.Domain.Entities;
using APIProduct.Domain.Exceptions.Exception;
using APIProduct.Domain.Mappers;
using Microsoft.EntityFrameworkCore;

namespace APIProduct.Domain.UseCases
{
    public class DeleteProductByNameUseCase
    {
        private readonly MyAppDbContext _context;
        private readonly ProductMapperDomain _productMapperDomain;
        private readonly Utlis.MyValidator _myValidator;

        public DeleteProductByNameUseCase(MyAppDbContext context, ProductMapperDomain productMapperDomain, Utlis.MyValidator myValidator)
        {
            _context = context;
            _productMapperDomain = productMapperDomain;
            _myValidator = myValidator;
        }

        public async Task<ProductDomain> Execute(string nameProduct)
        {
            if (!_myValidator.IsString(nameProduct)) {
                throw new ProductException("ProductName is required, please verify the information");
            }
            await Task.CompletedTask;
            var productToDelete = await _context.Products.FirstOrDefaultAsync(product => product.ProductName == nameProduct);
            if (productToDelete == null)
            {
                throw new ProductException("Product not found, please verify the information");
            }
            _context.Products.Remove(productToDelete);
            await _context.SaveChangesAsync();
            return _productMapperDomain.fromDataToDomain(productToDelete);
        }

    }
}
