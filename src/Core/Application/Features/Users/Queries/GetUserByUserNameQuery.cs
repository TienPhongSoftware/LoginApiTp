using Application.Dtos;
using Application.Interfaces.Repositories;
using Application.Wrappers.Concrete;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries
{
    public class GetUserByUserNameQuery : IRequest<DataResponse<UserDTO>>
    {
        [JsonIgnore]
        public string UserName { get; set; }

        public GetUserByUserNameQuery(string username)
        {
            UserName = username;
        }
        public class GetUserByUserNameQueryHandler : IRequestHandler<GetUserByUserNameQuery, DataResponse<UserDTO>>
        {
            private readonly IUserRepository _userRepository;
            private readonly ILogger<GetUserByUserNameQueryHandler> _logger;

            public GetUserByUserNameQueryHandler(IUserRepository userRepository, ILogger<GetUserByUserNameQueryHandler> logger)
            {
                _userRepository = userRepository;
                _logger = logger;
            }

            public async Task<DataResponse<UserDTO>> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetUserByUserNameAsync(request.UserName);
                _logger.LogInformation("GetUserByUserName = {@GetUserByUserName}", user);
                return new DataResponse<UserDTO>(user, 200);
            }
        }
    }
}
