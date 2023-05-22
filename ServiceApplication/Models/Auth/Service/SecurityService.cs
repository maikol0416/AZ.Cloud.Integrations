using Domain.Entities;
using Domain.Port;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceApplication.Base;
using ServiceApplication.Dto;
using ServiceApplication.Models.Auth.Mapper;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Util.Ex;

namespace ServiceApplication
{

    public class SecurityService : BaseServiceApplication<User, UserDto>, IBaseServiceApplication<User, UserDto>, ISecurityService
    {


        private readonly IConfiguration _configurate;
        private readonly IRolService _rolService;

        public SecurityService(IConfiguration configuration,
            IRolService rolService,
            ISecurityRepository securityRepository)
            : base(securityRepository)
        {
            _configurate = configuration;
            _rolService = rolService;
            CreateMapperExpresion<User, UserDto>(cnf =>
            {
                UserMapper.Expresion(cnf, rolService);
            });
        }

        public async Task<Login> Login(Login login)
        {
            var user = await this.FirstOrDefautlModelBy(f => (f.UserName == login.UserName || f.Email == login.UserName));
            if (user is null)
                throw new DomainException("El User " + login.UserName + " no ha sido encontrado");
            if (user.Password != login.Password)
                throw new DomainException("La contraseña del User " + login.UserName + " es incorrecta");
            if (user != null)
            {
                var authClaims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email)
                    };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurate["JWTMONGO:Secret"]));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(authClaims),
                    Issuer = _configurate["JWTMONGO:ValidIssuer"],
                    Audience = _configurate["JWTMONGO:ValidAudience"],

                    Expires = DateTime.Now.AddHours(24),
                    SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                Login log = new Login
                {
                    Token = tokenHandler.WriteToken(createdToken),
                    Expira = tokenDescriptor.Expires,
                    UserName = user.UserName,
                    Profile = new System.Collections.Generic.List<Rol>(),
                };
                log.Profile = user?.Roles;
                return log;
            }
            else
            {
                throw new DomainException("Usuario o contraseña invalidos");
            }
        }
    }
}
