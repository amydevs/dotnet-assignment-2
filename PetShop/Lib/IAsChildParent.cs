using Microsoft.AspNetCore.Components;

namespace PetShop.Lib;

/**
 * Interface used to strongly type a Parent component that supports the AsChild pattern inspired by https://www.radix-ui.com/primitives/docs/guides/composition
 */
public interface IAsChildParent
{
    public IDictionary<string, object>? AdditionalAttributes { get; set; }
    public RenderFragment<IDictionary<string, object>?>? AsChild { get; set; }
}