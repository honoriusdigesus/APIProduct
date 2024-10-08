using APIProduct.Data.Models;
using APIProduct.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace APIProduct.Domain.Utlis
{
    public class UtilsJwt
    {
        private readonly IConfiguration _configuration;

        public UtilsJwt(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Encrypt the token 
        public string encryptTokenSHA256(string token)
        {
            using (SHA256 SHA256Hash = SHA256.Create())
            {
                byte[] bytes = SHA256Hash.ComputeHash(Encoding.UTF8.GetBytes(token));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        //Generated JWT
        public string generateJwt(UserDomain user)
        {
            Claim[] userClaims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.IdentityDocument),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("FullName", user.FullName),  // Agregar FullName
                new Claim("LastName", user.LastName),  // Agregar LastName
                //new Claim("PasswordHash", user.PasswordHash),  // Agregar PasswordHash
                new Claim("RoleId", user.RoleId.ToString()),  // Si tienes RoleId
                new Claim("CreatedAt", user.CreatedAt?.ToString("o")) // Usar formato "o" para DateTime
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            //Create details of the token
            JwtSecurityToken jwtConfig = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }


        // Validate JWT
        public bool ValidateJwt(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /*

        // Get User from JWT
        public UserDomain GetUserFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var email = jwtToken.Claims.First(x => x.Type == ClaimTypes.Email).Value;

            return new UserDomain
            {
                UserId = userId,
                Email = email
            };
        }
        */

        public ClaimsPrincipal? validateJwt(string token)
        {
            // La clave simétrica que usaste para firmar el token
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            // Parámetros de validación del token
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false, // Si tienes un issuer, cámbialo a true y proporciona el issuer
                ValidateAudience = false, // Si tienes una audiencia, cámbialo a true y proporciona la audiencia
                ValidateLifetime = true, // Para verificar la expiración del token
                ClockSkew = TimeSpan.Zero // Evitar añadir tiempo extra para la expiración
            };

            try
            {
                // Validar y decodificar el token
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

                // Retornar los claims del usuario si la validación fue exitosa
                return principal;
            }
            catch (Exception)
            {
                // Si la validación falla, puedes retornar null o lanzar una excepción personalizada
                return null;
            }
        }
        public UserDomain? GetUserFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var claims = jwtToken.Claims;

            var user = new UserDomain
            {
                IdentityDocument = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                Email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                FullName = claims.FirstOrDefault(c => c.Type == "FullName")?.Value,  // Extraer FullName
                LastName = claims.FirstOrDefault(c => c.Type == "LastName")?.Value,  // Extraer LastName
                PasswordHash = claims.FirstOrDefault(c => c.Type == "PasswordHash")?.Value,  // Extraer PasswordHash
                CreatedAt = DateTime.TryParse(claims.FirstOrDefault(c => c.Type == "CreatedAt")?.Value, out var createdAt) ? createdAt : null,  // Extraer createdAt
                RoleId = int.TryParse(claims.FirstOrDefault(c => c.Type == "RoleId")?.Value, out var roleId) ? roleId : 0  // Extraer RoleId
            };

            if (user.IdentityDocument == null || user.Email == null)
            {
                // Si el token no contiene estos reclamos, devolvemos null
                return null;
            }

            return user;
        }



    }
}