using FluentValidation;
using MediatR;
using Warehouse360.Application.CatalogManagement.Commands;
using Warehouse360.Core.CatalogManagement.Entities;
using Warehouse360.Core.CatalogManagement.Repositories;
using Warehouse360.Core.SeedWork.Interfaces;

namespace Warehouse360.Application.CatalogManagement.Handlers;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly IValidator<CreateCategoryCommand> _validator;
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(
        IValidator<CreateCategoryCommand> validator, 
        ICategoryRepository categoryRepository)
    {
        _validator = validator;
        _categoryRepository = categoryRepository;
    }
    
    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        var category = new Category(request.Name, request.Description);
        
        await _categoryRepository.AddAsync(category, cancellationToken);
        
        return category.Id;
    }
}