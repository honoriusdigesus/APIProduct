namespace APIProduct.Presenter.Mappers
{
    public class RoleMapperPresenter
    {
        //From Domain to Presenter
        public APIProduct.Presenter.Entities.RolePresenter DomainToPresenter(APIProduct.Domain.Entities.RoleDomain role)
        {
            return new APIProduct.Presenter.Entities.RolePresenter
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName
            };
        }

        //From Presenter to Domain
        public APIProduct.Domain.Entities.RoleDomain PresenterToDomain(APIProduct.Presenter.Entities.RolePresenter role)
        {
            return new APIProduct.Domain.Entities.RoleDomain
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName
            };
        }
    }
}
