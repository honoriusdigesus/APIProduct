using APIProduct.Data.Context;
using APIProduct.Domain.Entities;
using APIProduct.Domain.Exceptions.Exception;
using APIProduct.Domain.Mappers;
using APIProduct.Domain.Utlis;
using Microsoft.AspNetCore.Mvc;

namespace APIProduct.Domain.UseCases
{
    public class CreateRoleUseCase
    {
        private readonly MyAppDbContext _context;
        private readonly RoleMapperDomain _roleMapperDomain;
        private readonly MyValidator _myValidator;

        public CreateRoleUseCase(MyAppDbContext context, RoleMapperDomain roleMapperDomain, MyValidator myValidator)
        {
            _context = context;
            _roleMapperDomain = roleMapperDomain;
            _myValidator = myValidator;
        }

        public async Task<RoleDomain> Execute(RoleDomain roleDomain)
        {
            //Validated fields of roleDomain
            if (!_myValidator.IsString(roleDomain.RoleName))
            {
                throw new RoleException("RoleName is required, please verify the information");
            }
            await Task.CompletedTask;
            var role = _roleMapperDomain.fromDomainToData(roleDomain);
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return _roleMapperDomain.fromDataToDomain(role);
        }
    }
}
