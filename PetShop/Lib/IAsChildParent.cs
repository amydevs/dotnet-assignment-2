using Microsoft.AspNetCore.Components;

namespace PetShop.Lib;

/**
 * Interface used to strongly type a Parent component that supports the AsChild pattern inspired by https://www.radix-ui.com/primitives/docs/guides/composition
 * Generic is used to define the type of the attributes provided.
 */
public interface IAsChildParent<TAttributes>
{
    public TAttributes? AdditionalAttributes { get; set; }
    public RenderFragment<TAttributes?>? AsChild { get; set; }
}