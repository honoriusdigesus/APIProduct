using APIProduct.Domain.Entities;

namespace APIProduct.Domain.Mappers
{
    public class RoleMapperDomain
    {
        //From Data to Domain
        public RoleDomain fromDataToDomain(APIProduct.Data.Models.Role role)
        {
            return new RoleDomain
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName
            };
        }

        //From Domain to Data
        public APIProduct.Data.Models.Role fromDomainToData(RoleDomain role)
        {
            return new APIProduct.Data.Models.Role
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName
            };
        }
    }
}
