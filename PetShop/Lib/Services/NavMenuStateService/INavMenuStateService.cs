namespace PetShop.Lib.Services.NavMenuStateService;

/**
 * Interface used to allow for mocked dependency injection
 */
public interface INavMenuStateService
{
    public NavMenuState State { get; set; }
    public Action? OnStateChanged { get; set; }
}