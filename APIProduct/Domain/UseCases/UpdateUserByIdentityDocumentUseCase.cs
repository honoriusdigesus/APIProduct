using APIProduct.Data.Context;
using APIProduct.Domain.Entities;
using APIProduct.Domain.Exceptions.Exception;
using APIProduct.Domain.Mappers;
using APIProduct.Domain.Utlis;
using Microsoft.EntityFrameworkCore;

namespace APIProduct.Domain.UseCases
{
    public class UpdateUserByIdentityDocumentUseCase
    {
        private readonly MyAppDbContext _context;
        private readonly UserMapperDomain _userMapperDomain;
        private readonly MyValidator _myValidator;

        public UpdateUserByIdentityDocumentUseCase(MyAppDbContext context, UserMapperDomain userMapperDomain, MyValidator myValidator)
        {
            _context = context;
            _userMapperDomain = userMapperDomain;
            _myValidator = myValidator;
        }

        public async Task<UserDomain> Execute(string identityDocument, UserDomain userDomain)
        {
            //Validated fields of identityDocument
            if (!_myValidator.IsIdentityDocument(identityDocument))
            {
                throw new UserException("Valid ID required, please verify information.");
            }
            await Task.CompletedTask;
            Data.Models.User? user = await _context.Users.FirstOrDefaultAsync(u => u.IdentityDocument == identityDocument);
            Console.WriteLine(user);
            if (user == null)
            {
                throw new UserException("User not found, please verify the information");
            }
            user.FullName = userDomain.FullName;
            user.LastName = userDomain.LastName;
            user.Email = userDomain.Email;
            user.PasswordHash = userDomain.PasswordHash;
            user.IdentityDocument = userDomain.IdentityDocument;
            user.RoleId = userDomain.RoleId;
            user.CreatedAt = userDomain.CreatedAt;
            user = _userMapperDomain.fromDomainToData(userDomain);
            await _context.SaveChangesAsync();
            return _userMapperDomain.fromDataToDomain(user);
        }
    }
}


/*
 
        public int UserId { get; set; }

        public string FullName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string IdentityDocument { get; set; } = null!;

        public int RoleId { get; set; }

        public DateTime? CreatedAt { get; set; }
 
 */