using APIProduct.Data.Context;
using APIProduct.Domain.Entities;
using APIProduct.Domain.Exceptions.Exception;
using APIProduct.Domain.Mappers;
using APIProduct.Domain.Utlis;
using System.Reflection.Metadata;

namespace APIProduct.Domain.UseCases
{
    public class CreateUserUseCase
    {
        private readonly MyAppDbContext _context;
        private readonly UserMapperDomain _userMapperDomain;
        private readonly GetRoleByIdUseCase _getRoleByIdUseCase;
        private readonly MyValidator _myValidator;
        private UtilsJwt _utilsJwt;

        public CreateUserUseCase(MyAppDbContext context, UserMapperDomain userMapperDomain, GetRoleByIdUseCase getRoleByIdUseCase, MyValidator myValidator, UtilsJwt utilsJwt)
        {
            _context = context;
            _userMapperDomain = userMapperDomain;
            _getRoleByIdUseCase = getRoleByIdUseCase;
            _myValidator = myValidator;
            _utilsJwt = utilsJwt;
        }
        public async Task<UserDomain> Execute(UserDomain userDomain) {
            if (!_myValidator.IsEmail(userDomain.Email))
            {
                throw new UserException("The email does not comply with the format, please verify the information");
            }

            if (!_myValidator.IsIdentityDocument(userDomain.IdentityDocument))
            {
                throw new UserException("Invalid document, it must have a minimum of 6 characters and a maximum of 10, it must not have periods or commas either, please verify the information");
            }

            if (!_myValidator.IsPassword(userDomain.PasswordHash))
            {
                throw new UserException("The password must contain at least one uppercase letter, one lowercase letter, one special character, and be between 8 and 16 characters long");
            }

            if (userDomain.RoleId<0) {
                throw new UserException("Invalid Role Id, please verify the information\"");
            }

            if (!_myValidator.IsString(userDomain.FullName) || !_myValidator.IsString(userDomain.LastName)) {
                throw new UserException("Invalid name, it must have a minimum of 3 characters and a maximum of 20, please verify the information");
            }

            RoleDomain role = await _getRoleByIdUseCase.Execute(userDomain.RoleId);


            if (role == null) {
                throw new UserException("Role not found, please verify the information");
            }

            userDomain.RoleId = role.RoleId;
            userDomain.PasswordHash = _utilsJwt.encryptTokenSHA256(userDomain.PasswordHash);
            Data.Models.User user = _userMapperDomain.fromDomainToData(userDomain);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return _userMapperDomain.fromDataToDomain(user);

        }
    }
}
