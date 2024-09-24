using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Warehouse360.Application.CatalogManagement.Abstractions;
using Warehouse360.Application.CatalogManagement.Dtos;

namespace Warehouse360.Redis.Services;

public class CategoryCacheService : ICategoryCacheService
{
    private readonly IDistributedCache _cache;
    private readonly string _categoryCacheKey = "categories";

    public CategoryCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        var cachedCategories = await _cache.GetStringAsync(_categoryCacheKey, cancellationToken);
        if (cachedCategories != null)
        {
            return JsonConvert.DeserializeObject<IEnumerable<CategoryDto>>(cachedCategories)!;
        }
        return null!;
    }

    public async Task SetCategoriesAsync(IEnumerable<CategoryDto> categories, CancellationToken cancellationToken)
    {
        var serializedCategories = JsonConvert.SerializeObject(categories);
        await _cache.SetStringAsync(_categoryCacheKey, serializedCategories, cancellationToken);
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var cachedCategories = await GetCategoriesAsync(cancellationToken);
        return cachedCategories.FirstOrDefault(c => c.Id == id)!;
    }

    public async Task SetCategoryAsync(CategoryDto category, CancellationToken cancellationToken)
    {
        var categories = (await GetCategoriesAsync(cancellationToken))?.ToList() ?? new List<CategoryDto>();
        categories.Add(category);
        await SetCategoriesAsync(categories, cancellationToken);
    }
}