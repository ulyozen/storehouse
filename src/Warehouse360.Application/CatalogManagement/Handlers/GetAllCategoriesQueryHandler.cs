using MediatR;
using Warehouse360.Application.CatalogManagement.Abstractions;
using Warehouse360.Application.CatalogManagement.Dtos;
using Warehouse360.Application.CatalogManagement.Queries;
using Warehouse360.Core.CatalogManagement.Repositories;
using Warehouse360.Core.SeedWork.Interfaces;

namespace Warehouse360.Application.CatalogManagement.Handlers;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
{
    private readonly ICategoryCacheService _categoryCacheService;
    private readonly ICategoryRepository _categoryRepository;

    public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, ICategoryCacheService categoryCacheService)
    {
        _categoryRepository = categoryRepository;
        _categoryCacheService = categoryCacheService;
    }
    
    public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var cachedCategories = await _categoryCacheService.GetCategoriesAsync(cancellationToken);

        if (cachedCategories != null) return cachedCategories;
        

        var categories = await _categoryRepository.GetAllAsync(cancellationToken);
        var categoryDtos = categories.Select(c => new CategoryDto(c.Id, c.Name, c.Description));

        await _categoryCacheService.SetCategoriesAsync(categoryDtos, cancellationToken);

        return categoryDtos;
    }
}