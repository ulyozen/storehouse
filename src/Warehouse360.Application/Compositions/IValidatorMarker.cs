namespace Warehouse360.Application.Compositions;

/// <summary>
/// Этот интерфейс не содержит логики и используется исключительно для указания сборки, в которой находятся валидаторы.
/// Его цель — облегчить автоматическую регистрацию всех валидаторов в контейнере Dependency Injection через метод
/// AddValidatorsFromAssemblyContaining()
/// </summary>
public interface IValidatorMarker;