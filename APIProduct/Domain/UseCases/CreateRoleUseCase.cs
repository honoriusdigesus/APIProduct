using APIProduct.Data.Context;
using APIProduct.Domain.Entities;
using APIProduct.Domain.Exceptions.Exception;
using APIProduct.Domain.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace APIProduct.Domain.UseCases
{
    public class CreateRoleUseCase
    {
        private readonly MyAppDbContext _context;
        private readonly RoleMapperDomain _roleMapperDomain;

        public CreateRoleUseCase(MyAppDbContext context, RoleMapperDomain roleMapperDomain)
        {
            _context = context;
            _roleMapperDomain = roleMapperDomain;
        }

        public async Task<RoleDomain> Execute(RoleDomain roleDomain)
        {
            //Validated fields of roleDomain
            if (string.IsNullOrEmpty(roleDomain.RoleName))
            {
                throw new RoleException("RoleName is required");
            }
            await Task.CompletedTask;
            var role = _roleMapperDomain.fromDomainToData(roleDomain);
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return _roleMapperDomain.fromDataToDomain(role);
        }
    }
}
