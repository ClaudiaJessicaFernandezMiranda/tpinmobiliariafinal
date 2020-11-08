using tpinmobiliariafinal.Models.Objetos;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace tpinmobiliariafinal.Models
{
    public static class DataValues
    {
        public static IConfiguration configuration;
        internal static string getHashed(string pass)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: pass,
                    salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8));
            return hashed;
        }
        internal static JwtSecurityToken getToken(Propietario u)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(configuration["TokenAuthentication:SecretKey"]));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, u.Mail),
                        new Claim("FullName", u.Nombre + " " + u.Apellido),
                        new Claim(ClaimTypes.Role, u.Id < 10? "Administrador":"Propietario"),
                    };
            var token = new JwtSecurityToken(
                issuer: configuration["TokenAuthentication:Issuer"],
                audience: configuration["TokenAuthentication:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credenciales
            );
            return token;
        }
    }
}

