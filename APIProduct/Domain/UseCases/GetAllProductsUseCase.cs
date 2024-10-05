using APIProduct.Data.Context;
using APIProduct.Domain.Entities;
using APIProduct.Domain.Mappers;
using Microsoft.EntityFrameworkCore;

namespace APIProduct.Domain.UseCases
{
    public class GetAllProductsUseCase
    {

        private readonly MyAppDbContext _context;
        private readonly ProductMapperDomain _productMapperDomain;

        public GetAllProductsUseCase(MyAppDbContext context, ProductMapperDomain productMapperDomain)
        {
            _context = context;
            _productMapperDomain = productMapperDomain;
        }

        public async Task<List<ProductDomain>> Execute()
        {
            List<Data.Models.Product> products = await _context.Products.ToListAsync();
            return products.Select(product => _productMapperDomain.fromDataToDomain(product)).ToList();
        }
    }
}
