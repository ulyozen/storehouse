using MediatR;
using Warehouse360.Application.CatalogManagement.Abstractions;
using Warehouse360.Application.CatalogManagement.Dtos;
using Warehouse360.Application.CatalogManagement.Queries;
using Warehouse360.Core.CatalogManagement.Repositories;
using Warehouse360.Core.SeedWork.Interfaces;

namespace Warehouse360.Application.CatalogManagement.Handlers;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly ICategoryCacheService _categoryCacheService;
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, ICategoryCacheService categoryCacheService)
    {
        _categoryRepository = categoryRepository;
        _categoryCacheService = categoryCacheService;
    }
    
    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var cachedCategory = await _categoryCacheService.GetCategoryByIdAsync(request.CategoryId, cancellationToken);
        if (cachedCategory != null)
        {
            return cachedCategory;
        }

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category == null)
        {
            throw new Exception($"Category with ID {request.CategoryId} not found.");
        }

        var categoryDto = new CategoryDto(category.Id, category.Name, category.Description);

        await _categoryCacheService.SetCategoryAsync(categoryDto, cancellationToken);

        return categoryDto;
    }
}