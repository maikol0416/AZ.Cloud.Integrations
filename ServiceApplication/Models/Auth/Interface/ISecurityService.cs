using Domain.Entities;
using ServiceApplication.Dto;
using System.Threading.Tasks;

namespace ServiceApplication
{
    public interface ISecurityService : IBaseServiceApplication<User, UserDto>
    {
        Task<Login> Login(Login login);

    }
}
