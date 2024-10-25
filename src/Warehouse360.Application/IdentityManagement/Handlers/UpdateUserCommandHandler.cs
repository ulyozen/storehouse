using MediatR;
using Warehouse360.Application.IdentityManagement.Abstractions;
using Warehouse360.Application.IdentityManagement.Commands;
using Warehouse360.Core.IdentityManagement.Repositories;
using Warehouse360.Core.IdentityManagement.Services;
using Warehouse360.Core.SeedWork.Interfaces;

namespace Warehouse360.Application.IdentityManagement.Handlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IUserCacheService _userCacheService;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IUserCacheService userCacheService)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _userCacheService = userCacheService;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                throw new ArgumentNullException("User not found: " + user);

            /*
             * Тут логика обновления пользователя
             */
            
            var result = await _userRepository.UpdateAsync(user);
            await _unitOfWork.CommitTransactionAsync();
            
            await _userCacheService.RemoveUserByIdAsync(request.UserId);
            
            return result;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}