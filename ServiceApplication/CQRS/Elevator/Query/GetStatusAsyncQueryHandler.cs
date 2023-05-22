using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using ServiceApplication.Dto;

namespace ServiceApplication.CQRS
{

    public record GetStatusQuery(string code) : IRequest<ElevatorStatusDto>;

    public class GetStatusAsyncQueryHandler : IRequestHandler<GetStatusQuery, ElevatorStatusDto>
    {
        protected readonly IElevatorService _implementation;

        public GetStatusAsyncQueryHandler(IElevatorService implementation)
        {
            _implementation = implementation;
        }

        public async Task<ElevatorStatusDto> Handle(GetStatusQuery request, CancellationToken cancellationToken)
        {
            return await _implementation.GetStatus(request.code);
        }
    }
}

