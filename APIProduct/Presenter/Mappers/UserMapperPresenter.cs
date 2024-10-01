using APIProduct.Domain.Entities;
using APIProduct.Presenter.Entities;

namespace APIProduct.Presenter.Mappers
{
    public class UserMapperPresenter
    {
        //From UserPresenter to UserDomain
        public UserDomain fromPresenterToDomain(UserPresenter userPresenter)
        {
            return new UserDomain
            {
                UserId = userPresenter.UserId,
                FullName = userPresenter.FullName,
                LastName = userPresenter.LastName,
                Email = userPresenter.Email,
                PasswordHash = userPresenter.PasswordHash,
                IdentityDocument = userPresenter.IdentityDocument,
                RoleId = userPresenter.RoleId,
                CreatedAt = userPresenter.CreatedAt
            };
        }

        //From UserDomain to UserPresenter
        public UserPresenter fromDomainToPresenter(UserDomain userDomain)
        {
            return new UserPresenter
            {
                UserId = userDomain.UserId,
                FullName = userDomain.FullName,
                LastName = userDomain.LastName,
                Email = userDomain.Email,
                PasswordHash = userDomain.PasswordHash,
                IdentityDocument = userDomain.IdentityDocument,
                RoleId = userDomain.RoleId,
                CreatedAt = userDomain.CreatedAt
            };
        }
    }
}
