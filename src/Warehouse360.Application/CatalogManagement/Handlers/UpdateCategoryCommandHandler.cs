using FluentValidation;
using MediatR;
using Warehouse360.Application.CatalogManagement.Commands;
using Warehouse360.Core.CatalogManagement.Repositories;
using Warehouse360.Core.SeedWork.Interfaces;

namespace Warehouse360.Application.CatalogManagement.Handlers;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly IValidator<UpdateCategoryCommand> _validator;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryCommandHandler(
        IValidator<UpdateCategoryCommand> validator, 
        ICategoryRepository categoryRepository, 
        IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category == null)
        {
            throw new Exception("Category not found");
        }

        category.UpdateProperties(request.Name, request.Description);
        await _unitOfWork.SaveChangeAsync(cancellationToken);
    }
}