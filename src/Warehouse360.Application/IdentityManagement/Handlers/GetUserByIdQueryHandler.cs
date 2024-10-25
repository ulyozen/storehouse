using MediatR;
using Warehouse360.Application.IdentityManagement.Abstractions;
using Warehouse360.Application.IdentityManagement.Dtos;
using Warehouse360.Application.IdentityManagement.Queries;
using Warehouse360.Core.IdentityManagement.Repositories;
using Warehouse360.Core.SeedWork.Interfaces;

namespace Warehouse360.Application.IdentityManagement.Handlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IUserCacheService _userCacheService;

    public GetUserByIdQueryHandler(
        IUnitOfWork unitOfWork, 
        IUserRepository userRepository, 
        IUserCacheService userCacheService)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _userCacheService = userCacheService;
    }
    
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var userCache = await _userCacheService.GetUserByIdAsync(request.UserId);
        if (userCache != null) return userCache;
        
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user is null)
                throw new ArgumentNullException("User not found: " + user);
            
            var userDto = new UserDto(user.Id, user.Username, user.Email.Value);

            await _userCacheService.SetUserByIdAsync(userDto, TimeSpan.FromDays(7));

            return userDto;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}