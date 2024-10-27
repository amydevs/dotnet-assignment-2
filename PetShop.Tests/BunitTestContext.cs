using Bunit;
using NUnit.Framework;
using PetShop.Lib.Services.NavMenuStateService;

namespace PetShop.Tests;

/// <summary>
/// Test context wrapper for bUnit.
/// Read more about using <see cref="BunitTestContext"/> <seealso href="https://bunit.dev/docs/getting-started/writing-tests.html#remove-boilerplate-code-from-tests">here</seealso>.
/// </summary>
public abstract class BunitTestContext : TestContextWrapper
{
	[SetUp]
	public void Setup()
	{
		TestContext = new Bunit.TestContext();
		TestContext.Services.AddDbContextFactory<ShopContext>();
		TestContext.Services.AddSingleton<INavMenuStateService>(new NavMenuStateService());
	}

	[TearDown]
	public void TearDown() => TestContext?.Dispose();
}
