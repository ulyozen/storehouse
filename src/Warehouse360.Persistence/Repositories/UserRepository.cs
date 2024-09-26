using Dapper;
using Warehouse360.Core.IdentityManagement.Entities;
using Warehouse360.Core.IdentityManagement.Repositories;
using Warehouse360.Core.SeedWork.Interfaces;
using Warehouse360.Persistence.Configurations;

namespace Warehouse360.Persistence.Repositories;

public class UserRepository(IUnitOfWork unitOfWork) : IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid id)
    {
        const string query = 
            $"""
             SELECT * FROM Users
             LEFT JOIN UserRoles ON Users.Id = UserRoles.UserId
             LEFT JOIN Roles ON UserRoles.RoleId = Roles.Id
             WHERE Id = @Id
             """;
        
        var userDictionary = new Dictionary<Guid, User>();

        await unitOfWork.Connection.QueryAsync<User, Role, User>(
            query,
            (user, role) =>
            {
                if (!userDictionary.TryGetValue(user.Id, out var currentUser))
                {
                    currentUser = user;
                    currentUser.InitialRole(new List<Role>());
                    userDictionary.Add(user.Id, currentUser);
                }

                currentUser.Roles.Add(role);
                return currentUser;
            }, new { Id = id });

        return userDictionary.Values.FirstOrDefault();
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        const string query = 
            $"""
             SELECT * FROM Users
             LEFT JOIN UserRoles ON Users.Id = UserRoles.UserId
             LEFT JOIN Roles ON UserRoles.RoleId = Roles.Id
             WHERE Username = @Username
             """;
        
        var userDictionary = new Dictionary<Guid, User>();

        await unitOfWork.Connection.QueryAsync<User, Role, User>(
            query,
            (user, role) =>
            {
                if (!userDictionary.TryGetValue(user.Id, out var currentUser))
                {
                    currentUser = user;
                    currentUser.InitialRole(new List<Role>());
                    userDictionary.Add(user.Id, currentUser);
                }

                currentUser.Roles.Add(role);
                return currentUser;
            }, new { Username = username });

        var result = userDictionary.Values.FirstOrDefault();
        
        return result;
    }

    public async Task<bool> AddAsync(User user)
    {
        const string query = 
            $"""
             INSERT INTO Users (Id, Username, Email, PasswordHash) 
             VALUES (@Id, @Username, @Email, @PasswordHash)
             """;
        var parameters = new { user.Username, user.Email.Value, user.PasswordHash, user.Id };
        
        var command = new CommandDefinition(query, parameters);
        
        var result = await unitOfWork.Connection.ExecuteAsync(command);
        
        return result > 0;
    }

    public async Task<bool> UpdateAsync(User user)
    {
        const string query = 
            $"""
             Update Users Set Username = @Username, Email = @Email, PasswordHash = @PasswordHash
             WHERE Id = @Id
             """;
        var parameters = new { user.Username, user.Email.Value, user.PasswordHash, user.Id };
        
        var command = new CommandDefinition(query, parameters);
        
        var result = await unitOfWork.Connection.ExecuteAsync(command);
        
        return result > 0;
    }

    public async Task<bool> DeleteAsync(User user)
    {
        const string query = 
            $"""
             DELETE FROM Users 
                    WHERE Id = @Id
             """;
        
        var command = new CommandDefinition(query, new { user.Id });
        
        var result = await unitOfWork.Connection.ExecuteAsync(command);
        
        return result > 0;
    }
}