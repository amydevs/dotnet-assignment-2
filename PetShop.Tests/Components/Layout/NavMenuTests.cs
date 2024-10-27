using System;
using System.Linq;
using Microsoft.AspNetCore.Components;
using PetShop.Components.Layout;
using PetShop.Lib.Services.NavMenuStateService;

namespace PetShop.Tests.Components.Layout;

/// <summary>
/// These tests are written entirely in C#.
/// Learn more at https://bunit.dev/docs/getting-started/writing-tests.html#creating-basic-tests-in-cs-files
/// </summary>
public class NavMenuTests : BunitTestContext
{
	[Test]
	public void DefaultState()
	{
		Services.GetService<INavMenuStateService>()!.State = NavMenuState.Default;
		// Arrange
		var cut = RenderComponent<NavMenu>();
		
		cut.Find("#mobile-nav").Matches(".hidden");
		cut.Find("#user-nav").Matches(".hidden");
		cut.Find("#cart-nav").Matches(".hidden");
	}
	
	[Test]
	public void NavOpenState()
	{
		Services.GetService<INavMenuStateService>()!.State = NavMenuState.NavOpen;
		// Arrange
		var cut = RenderComponent<NavMenu>();
		
		if (cut.Find("#mobile-nav").ClassList.Contains("hidden"))
		{
			throw new Exception("Nav Menu state is open");
		}
		cut.Find("#user-nav").Matches(".hidden");
		cut.Find("#cart-nav").Matches(".hidden");
	}
	
	[Test]
	public void UserOpenState()
	{
		Services.GetService<INavMenuStateService>()!.State = NavMenuState.UserOpen;
		// Arrange
		var cut = RenderComponent<NavMenu>();
		
		cut.Find("#mobile-nav").Matches(".hidden");
		if (cut.Find("#user-nav").ClassList.Contains("hidden"))
		{
			throw new Exception("User Menu state is open");
		}
		cut.Find("#cart-nav").Matches(".hidden");
	}
	
	[Test]
	public void CartOpenState()
	{
		Services.GetService<INavMenuStateService>()!.State = NavMenuState.CartOpen;
		// Arrange
		var cut = RenderComponent<NavMenu>();
		
		cut.Find("#mobile-nav").Matches(".hidden");
		cut.Find("#user-nav").Matches(".hidden");
		if (cut.Find("#cart-nav").ClassList.Contains("hidden"))
		{
			throw new Exception("Cart Menu state is open");
		}
	}
	
	[Test]
	public void CartStateUpdateTriggersAction()
	{
		Services.GetService<INavMenuStateService>()!.State = NavMenuState.CartOpen;
		var cbHasRan = false;
		var cb = () =>
		{
			cbHasRan = true;
		};
		Services.GetService<INavMenuStateService>()!.OnStateChanged += cb;
		Services.GetService<INavMenuStateService>()!.State = NavMenuState.Default;
		if (!cbHasRan)
		{
			throw new Exception("OnStateChanged was never triggered");
		}
		Services.GetService<INavMenuStateService>()!.OnStateChanged -= cb;
	}
}
