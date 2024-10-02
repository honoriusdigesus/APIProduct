using APIProduct.Domain.UseCases;
using APIProduct.Presenter.Entities;
using APIProduct.Presenter.Mappers;
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

        public LoginController(LoginUseCase loginUseCase, LoginMapperPresenter loginMapperPresenter)
        {
            _loginUseCase = loginUseCase;
            _loginMapperPresenter = loginMapperPresenter;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginPresenter loginPresenter)
        {
            var loginDomain = _loginMapperPresenter.fromPresenterToDomain(loginPresenter);
            return await _loginUseCase.Execute(loginDomain);
        }

    }
}
