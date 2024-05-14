using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using NetCoreYouTube.Models;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Nodes;

namespace NetCoreYouTube.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class UsuarioController : ControllerBase
    {
        public IConfiguration _configuration;
        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]


        public dynamic iniciarSesion([FromBody] object dataSesion)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(dataSesion.ToString());
            string user = data.usuario.ToString();
            string password = data.password.ToString();

            Usuario usuario = Usuario.DB().Where(x => x.usuario == user && x.password == password).FirstOrDefault();
            if (usuario == null)
            {
                return new
                {
                    success = false,
                    message = "Credenciales incorrectas",
                    result = "",
                };
            }

            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var claims = new[]
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id",usuario.idUsuario.ToString()),
                new Claim("usuario",usuario.usuario.ToString())
            };

            // se encripta la key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signing = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signing
                );

            return new
            {
                success = true,
                message = "Exito",
                result = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
