using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace PetShop;

public class ShopContext: DbContext
{
    public string DbPath { get; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<CartProduct> CartProducts { get; set; }
    
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    
    public DbSet<Booking> Bookings { get; set; }

    public ShopContext()
    {
        // var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Directory.GetCurrentDirectory();
        DbPath = Path.Join(path, "shop.db");
    }
    
    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options
            .UseSqlite($"Data Source={DbPath}")
            .UseLazyLoadingProxies()
            .UseSeeding((context, _) =>
            {
                var categories = context.Set<ProductCategory>().ToArray();
                if (!categories.Any())
                {
                    categories =
                    [
                        new ProductCategory
                        {
                            Name = "Accessories",
                            Description = "Focus on Your Summer Pet’s Accessories"
                        },
                        new ProductCategory
                        {
                            Name = "Foods",
                            Description = "Nourish Your Pet with the Best"
                        },
                        new ProductCategory
                        {
                            Name = "Other",
                            Description = "Your Other Necessities"
                        }
                    ];
                    foreach (var category in categories)
                    {
                        context.Set<ProductCategory>().Add(category);
                    }
                }
                if (!context.Set<Product>().Any())
                {
                    var productImageFolderPath = $"wwwroot/images/products";
                    Directory.CreateDirectory(productImageFolderPath);
            
                    var task = Task.Run(async () =>
                    {
                        var cookieContainer = new CookieContainer();
                        var baseAddress = new Uri("https://www.woolworths.com.au/");
                        using (var handler = new HttpClientHandler())
                        using (var client = new HttpClient(handler))
                        using (var imgClient = new HttpClient())
                        {
                            handler.CookieContainer = cookieContainer;
                            handler.AutomaticDecompression = DecompressionMethods.All;
                            client.BaseAddress = baseAddress;
                            client.DefaultRequestHeaders.UserAgent.ParseAdd(
                                "Mozilla/5.0 (X11; Linux x86_64; rv:122.0) Gecko/20100101 Firefox/122.0");
                            
                            await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, "/"));
                            
                            List<Task> tasks = new List<Task>();
                            
                            foreach (var category in categories)
                            {
                                var req = new HttpRequestMessage(HttpMethod.Post, "/apis/ui/Search/products");
                                req.Headers.Add("Accept", "application/json");
                                req.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                                req.Content =
                                    JsonContent.Create(new Dictionary<string, object>{
                                        { "SearchTerm", $"pet {category.Name}" },
                                        { "SortType", "TraderRelevance" },
                                        { "IsHideUnavailableProducts", true },
                                        { "PageSize", "10" },
                                    });
                                var resp = await client.SendAsync(req).ConfigureAwait(true);
                                resp.EnsureSuccessStatusCode();
                                dynamic body = JObject.Parse(await resp.Content.ReadAsStringAsync());
                                foreach (var productObj in body.Products)
                                {
                                    var product = productObj.Products[0];
                                    if (product.Price == null || product.SmallImageFile == null)
                                    {
                                        continue;
                                    }
                                    var productEntry = context.Set<Product>().Add(new Product
                                    {
                                        Description = product.FullDescription,
                                        Name = product.Name,
                                        Price = (int)(product.Price * 100),
                                        ProductCategory = category
                                    });
                                    context.SaveChanges();
                                    tasks.Add(Task.Run(async () =>
                                    {
                                        var res = await imgClient.GetAsync((string)product.MediumImageFile);
                                        using (var fs = new FileStream($"{productImageFolderPath}/{productEntry.Entity.Id}.png", FileMode.OpenOrCreate))
                                        {
                                            await res.Content.ReadAsStream().CopyToAsync(fs);
                                        }
                                    }));
                                    
                                }
                            }
                            await Task.WhenAll(tasks.ToArray());
                        }
                    });
                    task.Wait();
                }
            });
}

public enum PetType 
{
    Cat,
    Dog,
    Bird,
    Reptile
}

[Index(nameof(ProductId), IsUnique = true)]
public class CartProduct
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public virtual Product Product { get; set; }
    public int Quantity { get; set; }
}

public class ProductCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public virtual ProductCategory ProductCategory { get; set; }
}

public class Order
{
    public int Id { get; set; }
    [Display(Name = "Full Name"), Required]
    public string Name { get; set; }
    [Display(Name = "Address"), Required]
    public string Address { get; set; }
    [Display(Name = "Phone Number"), Required, RegularExpression("^[0-9]{8,10}$", ErrorMessage = "Phone number must be 8 to 10 digits")]
    public string PhoneNumber { get; set; }
    [Display(Name = "Email"), Required, EmailAddress]
    public string Email { get; set; }
    public int Total { get; set; }
}

[Index(nameof(ProductId), nameof(OrderId), IsUnique = true)]
public class OrderProduct
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public virtual Product Product { get; set; }
    public int OrderId { get; set; }
    [ForeignKey(nameof(OrderId))]
    public virtual Order Order { get; set; }
    public int Quantity { get; set; }
    public int GrossTotal { get; set; }
}

public enum BookingType
{
    VetAndCheckup,
    PetGrooming,
    PetSitting,
}

public class Booking
{
    public int Id { get; set; }
    [Display(Name = "Booking Type"), Required]
    public BookingType BookingType { get; set; }
    [Display(Name = "Owner Name"), Required]
    public string OwnerName { get; set; }
    [Display(Name = "Owner Phone Number"), Required, RegularExpression("^[0-9]{8,10}$", ErrorMessage = "Phone number must be 8 to 10 digits")]
    public string OwnerPhoneNumber { get; set; }
    [Display(Name = "Owner Email"), Required, EmailAddress]
    public string OwnerEmail { get; set; }
    [Display(Name = "Owner Address"), Required]
    public string OwnerAddress { get; set; }
    [Display(Name = "Pet Type"), Required]
    public PetType PetType { get; set; }
    [Display(Name = "Pet Name"), Required]
    public string PetName { get; set; }
    [Display(Name = "Pet Allergies")]
    public string PetAllergies { get; set; }
    [Display(Name = "Booking Date"), Required]
    public DateTime BookingDate { get; set; }
}