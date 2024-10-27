using Microsoft.AspNetCore.Components;

namespace PetShop.Lib;

public interface IAsChildParent
{
    public IDictionary<string, object>? AdditionalAttributes { get; set; }
    public RenderFragment<IDictionary<string, object>?>? AsChild { get; set; }
}