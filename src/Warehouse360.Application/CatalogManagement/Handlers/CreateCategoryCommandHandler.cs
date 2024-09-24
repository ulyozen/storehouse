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
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandHandler(
        IValidator<CreateCategoryCommand> validator, 
        ICategoryRepository categoryRepository, 
        IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        var category = new Category(request.Name, request.Description);
        
        await _categoryRepository.AddAsync(category, cancellationToken);
        
        await _unitOfWork.SaveChangeAsync(cancellationToken);
        
        return category.Id;
    }
}