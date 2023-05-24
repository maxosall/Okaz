using System.ComponentModel.DataAnnotations;

namespace Okaz.Models;

public class Product
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }

    [Required]
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }

    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
}

public class Category
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    // public string Email { get; set; }
    // public string PasswordHash { get; set; }
    // public virtual ICollection<Order> Orders { get; set; }
}

public class Order
{
    public int Id { get; set; }
    [Required]
    public DateTime Date { get; set; }
    public decimal TotalAmount { get; set; }
    // public int CustomerId { get; set; }
    // public Customer Customer { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
}

public class OrderDetail
{
    public int Id { get; set;}
    [Required]
    public int Quantity { get; set;}
    public decimal UnitPrice { get; set;}

    public int ProductId { get;set;}
    public virtual Product Product {get;set;}

    public int OrderId {get;set;}
    public virtual Order Order {get;set;}
}
// A class to represent a cart
public class Cart
{
    public Cart(int id, int useId)
    {
        Id = id;
        UserId = userId;
        CartItems = new List<CartItem>();
    }

    public int Id { get; }
    public int UserId { get; }

    public virtual User User { get; set; }
    public virtual ICollection<CartItem> CartItems { get; set; }
}

// A class to represent a cart item
public class CartItem
{
	public CartItem(int id, Quantity quantity, int productId, int cartId)
   	{
       Id = id;
       Quantity = quantity;
       ProductId = productId;
       CartId = cartId;
   	}
   	public int Id { get;}

   	[Required]
   	public Quantity Quantity {get;set;}

   	public int ProductId {get;}
   	public virtual Product Product {get;set;}

   	public int CartId {get;}
   	public virtual Cart Cart {get;set;}
}
