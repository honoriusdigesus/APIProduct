using APIProduct.Domain.UseCases;
using APIProduct.Presenter.Entities;
using APIProduct.Presenter.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProduct.Presenter.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly CreateProductUseCase _createProductUseCase;
        private readonly ProductMapperPresenter _productMapperPresenter;
        private readonly GetAllProductsUseCase _getAllProductsUseCase;
        private readonly GetProductByNameUseCase _getProductByNameUseCase;
        private readonly UpdateProductByNameUseCase _updateProductByNameUseCase;
        private readonly DeleteProductByNameUseCase _deleteProductByNameUseCase;

        public ProductController(
            CreateProductUseCase createProductUseCase,
            ProductMapperPresenter productMapperPresenter,
            GetAllProductsUseCase getAllProductsUseCase,
            GetProductByNameUseCase getProductByNameUseCase,
            UpdateProductByNameUseCase updateProductByNameUseCase,
            DeleteProductByNameUseCase deleteProductByNameUseCase
            )
        {
            _createProductUseCase = createProductUseCase;
            _productMapperPresenter = productMapperPresenter;
            _getAllProductsUseCase = getAllProductsUseCase;
            _getProductByNameUseCase = getProductByNameUseCase;
            _updateProductByNameUseCase = updateProductByNameUseCase;
            _deleteProductByNameUseCase = deleteProductByNameUseCase;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateProduct(ProductPresenter productPresenter)
        {
            var productDomain = _productMapperPresenter.fromPresenterToDomain(productPresenter);
            var product = await _createProductUseCase.Execute(productDomain);
            return Ok(_productMapperPresenter.fromDomainToPresenter(product));
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _getAllProductsUseCase.Execute();
            return Ok(products.Select(product => _productMapperPresenter.fromDomainToPresenter(product)));
        }


        [HttpGet]
        [Route("Get/{NameProduct}")]
        public async Task<IActionResult> GetProductByName(string NameProduct)
        {
            var product = await _getProductByNameUseCase.Execute(NameProduct);
            return Ok(_productMapperPresenter.fromDomainToPresenter(product));
        }

        [HttpPut]
        [Route("Update/{NameProduct}")]
        public async Task<IActionResult> UpdateProductByName(ProductPresenter productPresenter, string nameProduct )
        {
            var productDomain = _productMapperPresenter.fromPresenterToDomain(productPresenter);
            var product = await _updateProductByNameUseCase.Execute( productDomain, nameProduct);
            return Ok(_productMapperPresenter.fromDomainToPresenter(product));
        }

        [HttpDelete]
        [Route("Delete/{NameProduct}")]
        public async Task<IActionResult> DeleteProductByName(string NameProduct)
        {
            var product = await _deleteProductByNameUseCase.Execute(NameProduct);
            return Ok(_productMapperPresenter.fromDomainToPresenter(product));
        }
    }
}
