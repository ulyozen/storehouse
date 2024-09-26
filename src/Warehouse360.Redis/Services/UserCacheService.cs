using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Warehouse360.Application.IdentityManagement.Abstractions;
using Warehouse360.Application.IdentityManagement.Dtos;
using Warehouse360.Core.IdentityManagement.Entities;

namespace Warehouse360.Redis.Services;

public class UserCacheService : IUserCacheService
{
    private readonly IDistributedCache _distributedCache;
    private const string UserCacheKeyPrefix = "User:";

    public UserCacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }
    
    public async Task<UserDto?> GetUserByIdAsync(Guid userId)
    {
        var cacheKey = UserCacheKeyPrefix + userId;
        var cachedUser = await _distributedCache.GetStringAsync(cacheKey);
        
        if (string.IsNullOrEmpty(cachedUser)) return null!;

        return JsonConvert.DeserializeObject<UserDto>(cachedUser)!;
    }
    
    public async Task<User?> GetUserByNameAsync(string username)
    {
        var cacheKey = UserCacheKeyPrefix + username;
        var cachedUser = await _distributedCache.GetStringAsync(cacheKey);
        
        if (string.IsNullOrEmpty(cachedUser)) return null!;

        return JsonConvert.DeserializeObject<User>(cachedUser)!;
    }

    public async Task SetUserByIdAsync(UserDto user, TimeSpan expiration)
    {
        var cacheKey = UserCacheKeyPrefix + user.Id;
        var userJson = JsonConvert.SerializeObject(user);

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        };

        await _distributedCache.SetStringAsync(cacheKey, userJson, options);
    }
    
    public async Task SetUserByNameAsync(User user, TimeSpan expiration)
    {
        var cacheKey = UserCacheKeyPrefix + user.Username;
        var userJson = JsonConvert.SerializeObject(user);

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        };

        await _distributedCache.SetStringAsync(cacheKey, userJson, options);
    }

    public async Task RemoveUserByIdAsync(Guid userId)
    {
        var cacheKey = UserCacheKeyPrefix + userId;
        await _distributedCache.RemoveAsync(cacheKey);
    }
    
    public async Task RemoveUserByNameAsync(string username)
    {
        var cacheKey = UserCacheKeyPrefix + username;
        await _distributedCache.RemoveAsync(cacheKey);
    }
}