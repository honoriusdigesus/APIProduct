using APIProduct.Data.Context;
using APIProduct.Domain.Entities;
using APIProduct.Domain.Exceptions.Exception;
using APIProduct.Domain.Mappers;
using APIProduct.Domain.Utlis;
using Microsoft.EntityFrameworkCore;

namespace APIProduct.Domain.UseCases
{
    public class DeleteRoleByIdUseCase
    {

        private readonly MyAppDbContext _context;
        private readonly RoleMapperDomain _roleMapperDomain;

        public DeleteRoleByIdUseCase(MyAppDbContext context, RoleMapperDomain roleMapperDomain)
        {
            _context = context;
            _roleMapperDomain = roleMapperDomain;
        }

        public async Task<RoleDomain> Execute(int id)
        {
            if (id <= 0 )
            {
                throw new RoleException("Invalid Id, please verify the information");
            }
            await Task.CompletedTask;
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == id);
            if (role == null)
            {
                throw new RoleException("Role not found, please verify the information");
            }
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return _roleMapperDomain.fromDataToDomain(role);
        }
    }
}
