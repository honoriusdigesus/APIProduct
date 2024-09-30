using APIProduct.Data.Context;
using APIProduct.Domain.Entities;
using APIProduct.Domain.Exceptions.Exception;
using APIProduct.Domain.Mappers;
using Microsoft.EntityFrameworkCore;

namespace APIProduct.Domain.UseCases
{
    public class GetRoleByIdUseCase
    {
        private readonly MyAppDbContext _context;
        private readonly RoleMapperDomain _roleMapperDomain;

        public GetRoleByIdUseCase(MyAppDbContext context, RoleMapperDomain roleMapperDomain)
        {
            _context = context;
            _roleMapperDomain = roleMapperDomain;
        }

        public async Task<RoleDomain> Execute(int id)
        {
            if (id<=0) {
                throw new RoleException("Invalid Id");
            }
            await Task.CompletedTask;
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == id);
            if (role == null)
            {
                throw new RoleException("Role not found");
            }
            return _roleMapperDomain.fromDataToDomain(role);
        }
    }
}
