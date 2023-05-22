using System.Collections.Generic;
using Domain.Common;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ServiceApplication.Base;
using ServiceApplication.CQRS;
using ServiceApplication.Dto;
using ServiceApplication.Models.Auth.Validator;
using Util.Common;

namespace ServiceApplication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjectionsApplications(this IServiceCollection services)
        {
            services.AddScoped<IValidator<RolDto>, RolValidator>();
            services.AddScoped<IValidator<UserDto>, SecurityValidator>();
            services.AddScoped<IValidator<ElevatorDto>, ElevatorValidator>();
            services.AddScoped<IValidator<ElevatorMovementDto>, ElevatorManagementValidator>();
            services.AddTransient<IElevatorService,ElevatorService>();
            services.AddTransient<IElevatorMovementService, ElevatorMovementService>();
            return services;
        }

        public static IServiceCollection AddMediatrDependecyInjection(this IServiceCollection services)
        {

            services.RegisterMediatrCustom();

            services.RegisterMediatrAbstractService<SecurityService, UserDto, User, ISecurityService>();
            services.RegisterMediatrAbstractService<RolService, RolDto, Rol, IRolService>();
            services.RegisterMediatrAbstractService<ElevatorService, ElevatorDto, Elevator, IElevatorService>();
            services.RegisterMediatrAbstractService<ElevatorMovementService, ElevatorMovementDto, ElevatorMovement, IElevatorMovementService>();

            return services;
        }

        public static void RegisterMediatrAbstractService<Service, DTO, ENT, TImplementation>(this IServiceCollection services)
            where Service : BaseServiceApplication<ENT, DTO>
            where DTO : class, new()
            where ENT : BaseEntity, new()
            where TImplementation : IBaseServiceApplication<ENT, DTO>
        {
            services.AddScoped(typeof(TImplementation), typeof(Service));
            services.AddScoped(typeof(IBaseServiceApplication<ENT, DTO>), typeof(Service));
            services.AddMediatR(typeof(CreateAsyncCommandHandler<ENT, DTO>));
            services.AddScoped(typeof(IRequestHandler<CreateAsyncCommand<ENT, DTO>, DTO>),typeof(CreateAsyncCommandHandler<ENT, DTO>));
            services.AddMediatR(typeof(UpdateAsyncCommandHandler<ENT, DTO>));
            services.AddScoped(typeof(IRequestHandler<UpdateAsyncCommand<ENT, DTO>, DTO>), typeof(UpdateAsyncCommandHandler<ENT, DTO>));
            services.AddMediatR(typeof(DeleteAsyncCommandHandler<ENT, DTO>));
            services.AddScoped(typeof(IRequestHandler<DeleteAsyncCommand<ENT, DTO>, bool>), typeof(DeleteAsyncCommandHandler<ENT, DTO>));
            services.AddMediatR(typeof(ToListAsyncQueryHandler<ENT, DTO>));
            services.AddScoped(typeof(IRequestHandler<ToListAsyncQuery<ENT, DTO>, List<DTO>>), typeof(ToListAsyncQueryHandler<ENT, DTO>));
            services.AddMediatR(typeof(PaginateAsyncQueryHandler<ENT, DTO>));
            services.AddScoped(typeof(IRequestHandler<PaginateAsyncQuery<ENT, DTO>, Paginate<DTO>>), typeof(PaginateAsyncQueryHandler<ENT, DTO>));
            services.AddMediatR(typeof(SearchAsyncQueryHandler<ENT, DTO>));
            services.AddScoped(typeof(IRequestHandler<SearchAsyncQuery<ENT, DTO>, DTO>), typeof(SearchAsyncQueryHandler<ENT, DTO>));
            services.AddMediatR(typeof(SearchListAsyncQueryHandler<ENT, DTO>));
            services.AddScoped(typeof(IRequestHandler<SearchListAsyncQuery<ENT, DTO>, List<DTO>>), typeof(SearchListAsyncQueryHandler<ENT, DTO>));

        }

        public static void RegisterMediatrCustom(this IServiceCollection services)
        {
            services.AddMediatR(typeof(LoginAsyncQueryHandler));
            services.AddScoped(typeof(IRequestHandler<LoginAsyncQuery, Login>), typeof(LoginAsyncQueryHandler));

        }
    }
}
