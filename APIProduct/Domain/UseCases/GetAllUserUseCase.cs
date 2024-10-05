using APIProduct.Data.Context;
using APIProduct.Domain.Entities;
using APIProduct.Domain.Exceptions.Exception;
using APIProduct.Domain.Mappers;
using Microsoft.EntityFrameworkCore;

namespace APIProduct.Domain.UseCases
{
    public class GetAllUserUseCase
    {

        private readonly MyAppDbContext _context;
        private readonly UserMapperDomain _userMapperDomain;

        public GetAllUserUseCase(MyAppDbContext context, UserMapperDomain userMapperDomain)
        {
            _context = context;
            _userMapperDomain = userMapperDomain;
        }


        public async Task<List<UserDomain>> Execute()
        {
            await Task.CompletedTask;
            List<Data.Models.User> users = await _context.Users.ToListAsync();
            //Validamos si la lista de roles esta vacia
            if (users.Count == 0)
            {
                throw new UserException("There are no registered users in the database");
            }
            return users.Select(user => _userMapperDomain.fromDataToDomain(user)).ToList();
        }
    }
}

