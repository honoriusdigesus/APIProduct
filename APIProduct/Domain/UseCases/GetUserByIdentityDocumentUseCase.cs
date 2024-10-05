using APIProduct.Data.Context;
using APIProduct.Domain.Entities;
using APIProduct.Domain.Exceptions.Exception;
using APIProduct.Domain.Mappers;
using APIProduct.Domain.Utlis;
using Microsoft.EntityFrameworkCore;

namespace APIProduct.Domain.UseCases
{
    public class GetUserByIdentityDocumentUseCase
    {
        private readonly MyAppDbContext _context;
        private readonly UserMapperDomain _userMapperDomain;
        private readonly MyValidator _myValidator;

        public GetUserByIdentityDocumentUseCase(MyAppDbContext context, UserMapperDomain userMapperDomain, MyValidator myValidator)
        {
            _context = context;
            _userMapperDomain = userMapperDomain;
            _myValidator = myValidator;
        }


        public async Task<UserDomain> Execute(string identityDocument)
        {
            //Validated fields of identityDocument
            if (!_myValidator.IsIdentityDocument(identityDocument))
            {
                throw new UserException("Valid ID required, please verify information.");
            }
            await Task.CompletedTask;
            Data.Models.User? user = await _context.Users.FirstOrDefaultAsync(u => u.IdentityDocument == identityDocument);
            if (user == null)
            {
                throw new UserException("User not found, please verify the information");
            }
            return _userMapperDomain.fromDataToDomain(user);
        }
    }
}
