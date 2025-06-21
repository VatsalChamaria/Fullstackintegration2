using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProductManagment;
builder.Services.AddCors();
builder.Services.AddScoped<ProductService>();
builder.Services.AddMemoryCache();

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
await builder.Build().RunAsync();
app.MapGet("/api/productlist", () => { /* return data */ });

app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

          try
{
    var response = await HttpClient.GetAsync("/api/productlist");
    response.EnsureSuccessStatusCode();
    var json = await response.Content.ReadAsStringAsync();
    products = JsonSerializer.Deserialize<Product[]>(json);
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
    // Optionally show a fallback UI or message in the front-end
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
    public Category Category { get; set; }
}

app.MapGet("/api/productlist", () =>
{
    return new List<Product>
    {
        new()
        {
            Id = 1,
            Name = "Laptop",
            Price = 1200.50,
            Stock = 25,
            Category = new Category { Id = 101, Name = "Electronics" }
        },
        new()
        {
            Id = 2,
            Name = "Headphones",
            Price = 50.00,
            Stock = 100,
            Category = new Category { Id = 102, Name = "Accessories" }
        }
    };
});
