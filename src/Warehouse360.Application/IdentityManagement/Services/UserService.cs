using Warehouse360.Core.IdentityManagement.Entities;
using Warehouse360.Core.IdentityManagement.Repositories;
using Warehouse360.Core.IdentityManagement.Services;
using Warehouse360.Core.IdentityManagement.ValueObjects;
using Warehouse360.Core.SeedWork.Interfaces;

namespace Warehouse360.Application.IdentityManagement.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> CreateUser(string username, Email email, string password)
    {
        var hash = _passwordHasher.HashPassword(password);
        var user = new User(username, email, hash);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CommitTransactionAsync();
            return user;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task AssignRole(User user, Role role)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            user.AssignRole(role);
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
    
    public async Task ChangePassword(Guid userId, string password)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            var hashedPassword = _passwordHasher.HashPassword(password);
            user.ChangePassword(hashedPassword);

            await _userRepository.UpdateAsync(user);
            await _unitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}