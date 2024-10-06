using APIProduct.Domain.UseCases;
using APIProduct.Presenter.Entities;
using APIProduct.Presenter.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProduct.Presenter.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginUseCase _loginUseCase;
        private readonly LoginMapperPresenter _loginMapperPresenter;
        private readonly ValidateTokenUseCase _validateTokenUseCase;

        public LoginController(LoginUseCase loginUseCase, LoginMapperPresenter loginMapperPresenter, ValidateTokenUseCase validateTokenUseCase)
        {
            _loginUseCase = loginUseCase;
            _loginMapperPresenter = loginMapperPresenter;
            _validateTokenUseCase = validateTokenUseCase;
        }


        [HttpPost]
        [Route("Auth")]
        public async Task<IActionResult> Login(LoginPresenter loginPresenter)
        {
            Domain.Entities.LoginDomain loginDomain = _loginMapperPresenter.fromPresenterToDomain(loginPresenter);
            return await _loginUseCase.Execute(loginDomain);
        }

        [HttpPost]
        [Route("ValidateToken")]
        public IActionResult ValidateToken()
        {
            if (!Request.Headers.TryGetValue("Authorization", out var token))
            {
                return BadRequest("Authorization header is missing");
            }

            // Remove "Bearer " prefix if present
            var tokenValue = token.ToString().StartsWith("Bearer") ? token.ToString().Substring(7) : token.ToString();

            return _validateTokenUseCase.Execute(tokenValue);
        }

    }
}
