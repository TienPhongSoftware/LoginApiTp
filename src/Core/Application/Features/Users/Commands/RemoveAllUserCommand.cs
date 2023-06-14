using Application.Constants;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Wrappers.Abstract;
using Application.Wrappers.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands
{
    public class RemoveAllUserCommand : IRequest<IResponse>
    {

        public class RemoveAllUserCommandHandler : IRequestHandler<RemoveAllUserCommand, IResponse>
        {
            private readonly IUserRepository _userRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IEasyCacheService _easyCacheService;

            public RemoveAllUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IEasyCacheService easyCacheService)
            {
                _userRepository = userRepository;
                _unitOfWork = unitOfWork;
                _easyCacheService = easyCacheService;
            }

            public async Task<IResponse> Handle(RemoveAllUserCommand request, CancellationToken cancellationToken)
            {
                var users = await _userRepository.GetAllAsync();
                if (users == null)
                {
                    throw new ApiException(404, Messages.UserNotFound);
                }
                else
                {
                    foreach (var user in users)
                    {
                        _userRepository.Remove(user);
                        await _unitOfWork.SaveChangesAsync();
                        await _easyCacheService.RemoveByPrefixAsync("GetAuthenticatedUserWithRoles");
                    }
                }
                return new SuccessResponse(200, Messages.DeletedSuccessfully);
            }
        }
    }
}
