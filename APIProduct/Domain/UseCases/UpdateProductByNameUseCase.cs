using APIProduct.Data.Context;
using APIProduct.Domain.Entities;
using APIProduct.Domain.Exceptions.Exception;
using APIProduct.Domain.Mappers;
using Microsoft.EntityFrameworkCore;

namespace APIProduct.Domain.UseCases
{
    public class UpdateProductByNameUseCase
    {
        private readonly MyAppDbContext _context;
        private readonly ProductMapperDomain _productMapperDomain;
        private readonly Utlis.MyValidator _myValidator;

        public UpdateProductByNameUseCase(MyAppDbContext context, ProductMapperDomain productMapperDomain, Utlis.MyValidator myValidator)
        {
            _context = context;
            _productMapperDomain = productMapperDomain;
            _myValidator = myValidator;
        }

        public async Task<ProductDomain> Execute(ProductDomain newProductDomain, string nameProductOld)
        {
            //Validated fields of productDomain
            if (!_myValidator.IsString(newProductDomain.ProductName))
            {
                throw new ProductException("ProductName is required, please verify the information");
            }
            if (newProductDomain.Price < 0 || newProductDomain.Price.Equals(""))
            {
                throw new ProductException("Price is required, please verify the information");
            }
            await Task.CompletedTask;
            Data.Models.Product product = _productMapperDomain.fromDomainToData(newProductDomain);
            Data.Models.Product? productToUpdate = await _context.Products.FirstOrDefaultAsync(product => product.ProductName == nameProductOld);
            if (productToUpdate == null)
            {
                throw new ProductException("Product not found, please verify the information");
            }
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.Price = product.Price;
            productToUpdate.CreatedBy = product.CreatedBy;
            productToUpdate.CreatedAt = product.CreatedAt;
            await _context.SaveChangesAsync();
            return _productMapperDomain.fromDataToDomain(productToUpdate);
        }

    }
}
