using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;

namespace ServiceApplication.CQRS
{

    public record LoginAsyncQuery(Login Login) : IRequest<Login>;

    public class LoginAsyncQueryHandler : IRequestHandler<LoginAsyncQuery, Login>
    {
        protected readonly ISecurityService _implementation;

        public LoginAsyncQueryHandler(ISecurityService implementation)
        {
            _implementation = implementation;
        }

        public async Task<Login> Handle(LoginAsyncQuery request, CancellationToken cancellationToken)
        {
            return await _implementation.Login(request.Login);
        }
    }
}

