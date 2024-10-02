using APIProduct.Domain.Entities;
using APIProduct.Presenter.Entities;

namespace APIProduct.Presenter.Mappers
{
    public class LoginMapperPresenter
    {
        public LoginDomain fromPresenterToDomain(LoginPresenter loginPresenter)
        {
            return new LoginDomain
            {
                Email = loginPresenter.Email,
                Password = loginPresenter.Password
            };
        }

        public LoginPresenter fromDomainToPresenter(LoginDomain loginDomain)
        {
            return new LoginPresenter
            {
                Email = loginDomain.Email,
                Password = loginDomain.Password
            };
        }
    }
}
