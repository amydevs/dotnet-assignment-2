using PetShop;
using PetShop.Components;
using PetShop.Lib.Services.NavMenuStateService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add DB Service to the container.
builder.Services.AddDbContextFactory<ShopContext>();

// Add NavMenuStateService to the container for managing NavMenu re=renders.
builder.Services.AddSingleton<INavMenuStateService, NavMenuStateService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
