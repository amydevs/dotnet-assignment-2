namespace PetShop.Lib.Services.NavMenuStateService;

public class NavMenuStateService: INavMenuStateService
{
    private NavMenuState _state;
    public NavMenuState State
    {
        get => _state;
        set
        {
            if (_state != value)
            {
                _state = value;
                OnStateChanged?.Invoke();
            }
        }
    }

    public Action? OnStateChanged { get; set; }
    
    public NavMenuStateService()
    {
        _state = NavMenuState.Default;
    }
}