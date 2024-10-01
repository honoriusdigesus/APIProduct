using APIProduct.Data.Models;
using APIProduct.Domain.Entities;

namespace APIProduct.Domain.Mappers
{
    public class UserMapperDomain
    {
        //From Data to UserDomain
        public UserDomain fromDataToDomain(User user) {
            return new UserDomain
            {
                UserId = user.UserId,
                FullName = user.FullName,
                LastName = user.LastName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                IdentityDocument = user.IdentityDocument,
                RoleId = user.RoleId,
                CreatedAt = user.CreatedAt
            };
        }

        //From UserDomain to Data
        public User fromDomainToData(UserDomain userDomain)
        {
            return new User
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
