using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebCore.Models
{
    public class JwtManager
    {
        /// <summary>
        /// parametros de la clase
        /// </summary>
        private readonly string _secret;
        private readonly string _expDate;

        /// <summary>
        /// constructor con parametros de la clase
        /// </summary>
        /// <param name="config"></param>
        public JwtManager(IConfiguration config)
        {
            _secret = config.GetSection("JwtConfig").GetSection("secret").Value;
            _expDate = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;
        }
        /// <summary>
        /// Metodo para crear el token de seguridad
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string GenerateSecurityToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expDate)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
