using APIProduct.Data.Context;
using APIProduct.Domain.Entities;
using APIProduct.Domain.Exceptions.Exception;
using APIProduct.Domain.Mappers;
using Microsoft.EntityFrameworkCore;

namespace APIProduct.Domain.UseCases
{
    public class UpdateRoleUseCase
    {
        private readonly MyAppDbContext _context;
        private readonly RoleMapperDomain _roleMapperDomain;

        public UpdateRoleUseCase(MyAppDbContext context, RoleMapperDomain roleMapperDomain)
        {
            _context = context;
            _roleMapperDomain = roleMapperDomain;
        }

        public async Task<RoleDomain> Execute(RoleDomain roleDomain, int roleId)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == roleId);
            if (role == null)
            {
                throw new RoleException("Role not found");
            }
            if (role.RoleName == roleDomain.RoleName)
            {
                throw new RoleException("The new role name cannot be the same as the current name. Please enter a different name.");
            }


            role.RoleName = roleDomain.RoleName;
            await _context.SaveChangesAsync();
            return _roleMapperDomain.fromDataToDomain(role);
        }
    }
}
