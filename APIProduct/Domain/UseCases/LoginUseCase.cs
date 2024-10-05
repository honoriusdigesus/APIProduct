using APIProduct.Data.Context;
using APIProduct.Domain.Entities;
using APIProduct.Domain.Exceptions.Exception;
using APIProduct.Domain.Mappers;
using APIProduct.Domain.Utlis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIProduct.Domain.UseCases
{
    public class LoginUseCase
    {
        private readonly MyAppDbContext _context;
        private readonly UserMapperDomain _userMapperDomain;
        private readonly MyValidator _myValidator;
        private readonly UtilsJwt _utilsJwt;

        public LoginUseCase(MyAppDbContext context, UserMapperDomain userMapperDomain, MyValidator myValidator, UtilsJwt utilsJwt)
        {
            _context = context;
            _userMapperDomain = userMapperDomain;
            _myValidator = myValidator;
            _utilsJwt = utilsJwt;
        }

        public async Task<IActionResult> Execute(LoginDomain loginDomain)
        {
            //Validated fields of email and password
            if (!_myValidator.IsEmail(loginDomain.Email))
            {
                throw new UserException("Valid email required, please verify information.");
            }
            if (!_myValidator.IsPassword(loginDomain.Password))
            {
                throw new UserException("Valid password required, please verify information.");
            }
            await Task.CompletedTask;
            Data.Models.User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDomain.Email && u.PasswordHash == _utilsJwt.encryptTokenSHA256(loginDomain.Password));
            if (user == null)
            {
                throw new UserException("User not found, please verify the information");
            }
            //Return token y user
            return new OkObjectResult(new {  user = _userMapperDomain.fromDataToDomain(user), token = _utilsJwt.generateJwt(_userMapperDomain.fromDataToDomain(user)) });

        }
    }
}

