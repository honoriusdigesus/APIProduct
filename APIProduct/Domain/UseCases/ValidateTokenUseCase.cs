using APIProduct.Domain.Exceptions.Exception;
using APIProduct.Domain.Utlis;
using Microsoft.AspNetCore.Mvc;

namespace APIProduct.Domain.UseCases
{
    public class ValidateTokenUseCase
    {
        private readonly UtilsJwt _utilsJwt;

        public ValidateTokenUseCase(UtilsJwt utilsJwt)
        {
            _utilsJwt = utilsJwt;
        }

        public IActionResult Execute(string token)
        {
            if (!_utilsJwt.ValidateJwt(token))
            {
                throw new UserException("Invalid token, please verify the information");
            }

            Entities.UserDomain user = _utilsJwt.GetUserFromToken(token);
            return new OkObjectResult(user);
        }

    }
}
