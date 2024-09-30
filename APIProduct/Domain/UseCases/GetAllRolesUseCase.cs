using APIProduct.Data.Context;
using APIProduct.Domain.Entities;
using APIProduct.Domain.Exceptions.Exception;
using APIProduct.Domain.Mappers;
using Microsoft.EntityFrameworkCore;

namespace APIProduct.Domain.UseCases
{
    public class GetAllRolesUseCase
    {
        private readonly MyAppDbContext _context;
        private readonly RoleMapperDomain _roleMapperDomain;

        public GetAllRolesUseCase(MyAppDbContext context, RoleMapperDomain roleMapperDomain)
        {
            _context = context;
            _roleMapperDomain = roleMapperDomain;
        }

        public async Task<List<RoleDomain>> Execute()
        {
            await Task.CompletedTask;
            var roles = await _context.Roles.ToListAsync();
            //Validamos si la lista de roles esta vacia
            if (roles.Count == 0)
            {
                throw new RoleException("No roles found");
            }
            return roles.Select(role => _roleMapperDomain.fromDataToDomain(role)).ToList();
        }
    }
}
