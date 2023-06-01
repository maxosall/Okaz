using Microsoft.EntityFrameworkCore;
using Okaz.Models;
using Bogus;

namespace Okaz.Okaz.API.Models;

public class OkazDbContext: DbContext
{
    // Faker<Product> productFaker;
    // Faker<Category> categoryFaker;
    // Faker<User> userFaker;
    // Faker<Cart> cartFaker;
    // Faker<CartItem> cartItemFaker;

    // List<Product> products;
    // List<Category> categories;
    // List<User> users;
    // List<Cart> carts;
    // List<CartItem> cartItems;


	public OkazDbContext(DbContextOptions<OkazDbContext> options)
        :base(options)
    {
        ChangeTracker.AutoDetectChangesEnabled = false;

        // productFaker = new Faker<Product>()
        // .RuleFor(p => p.ProductId, f => f.IndexFaker)
        // .RuleFor(p => p.Name, f => f.Commerce.ProductName())
        // .RuleFor(p => p.Description, f => f.Lorem.Sentence())
        // .RuleFor(p => p.Price, f => f.Random.Decimal(0.01m, 1000m))
        // .RuleFor(p => p.ImageUrl, f => f.Image.PicsumUrl())
        // .RuleFor(p => p.CategoryId, f => f.Random.Int(1, 10));

        // categoryFaker = new Faker<Category>()
        //     .RuleFor(c => c.CategoryId, f => f.IndexFaker)
        //     .RuleFor(c => c.Name, f => f.Commerce.Categories(1).First());

        // userFaker = new Faker<User>()
        //     .RuleFor(u => u.UserId, f => f.IndexFaker)
        //     .RuleFor(u => u.Name, f => f.Name.FullName());

        // cartFaker = new Faker<Cart>()
        //     .RuleFor(c => c.CartId, f => f.IndexFaker)
        //     .RuleFor(c => c.UserId, (f,c) => userFaker.Generate().UserId);

        // cartItemFaker = new Faker<CartItem>()
        //     .RuleFor(ci => ci.CartItemId, f => f.IndexFaker)
        //     .RuleFor(ci => ci.Quantity, f => f.Random.Int(1, 10))
        //     .RuleFor(ci => ci.ProductId, (f,ci) => productFaker.Generate().ProductId)
        //     .RuleFor(ci => ci.CartId, (f,ci) => cartFaker.Generate().CartId);

        // // Generate a list of fake data for each entity
        // products = productFaker.Generate(20);
        // categories = categoryFaker.Generate(10);
        // users = userFaker.Generate(5);
        // carts = cartFaker.Generate(5);
        // cartItems = cartItemFaker.Generate(15);
    }

    public DbSet<Product> Products{ get; set; }
    public DbSet<User> Users{ get; set; }
    public DbSet<Category> Categories{ get; set; }
    public DbSet<CartItem> CartItems{ get; set; }
    public DbSet<Cart> Carts{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the one-to-many relationship between Category and Product
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Products) 
            .WithOne(p => p.Category) 
            .HasForeignKey(p => p.CategoryId) 
            .OnDelete(DeleteBehavior.SetNull); 

        modelBuilder.Entity<Product>()
            .HasOne(e => e.Category)
            .WithMany(e => e.Products)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
            
        // Configure the one-to-many relationship between Product and CartItem
        modelBuilder.Entity<Product>()
            .HasMany(p => p.CartItems) 
            .WithOne(c => c.Product) 
            .HasForeignKey(c => c.ProductId) 
            .OnDelete(DeleteBehavior.Cascade); 

        // Configure the one-to-one relationship between User and Cart
        modelBuilder.Entity<User>()
            .HasOne(u => u.Cart)  
            .WithOne(c => c.User) 
            .HasForeignKey<Cart>(c => c.UserId) 
            .OnDelete(DeleteBehavior.Cascade); 

        // Configure the one-to-many relationship between Cart and CartItem
        modelBuilder.Entity<Cart>()
            .HasMany(c => c.CartItems) 
            .WithOne(i => i.Cart) 
            .HasForeignKey(i => i.CartId) 
            .OnDelete(DeleteBehavior.Cascade);     
    }   
}

