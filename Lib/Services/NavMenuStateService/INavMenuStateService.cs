namespace PetShop.Lib.Services.NavMenuStateService;

public interface INavMenuStateService
{
    public NavMenuState State { get; set; }
    public Action? OnStateChanged { get; set; }
}