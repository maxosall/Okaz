using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Okaz.Models;

public class Product
{
    [Key]
    public int ProductId { get; set; }

    [StringLength(150, MinimumLength = 3, ErrorMessage = "The name must be between 3 and 150 characters.")]    [Required]
    public string Name { get; set; }
  
    [Required]
    [StringLength(500, ErrorMessage = "The description cannot exceed 500 characters.")]
    public string Description { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    [Url]
    public string ImageUrl { get; set; }

    [ForeignKey("Category")]
    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }
    public virtual ICollection<CartItem> CartItems { get; set; }
}


public class Category
{
    public int CategoryId { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    public virtual ICollection<Product>? Products { get; set; }
}

public class User
{
    public int UserId { get; set; }
    [Required]
    public string Name { get; set; }

    [ForeignKey("Cart")]
    public int? CartId { get; set; }
    public virtual Cart? Cart { get; set; }
    // public string Email { get; set; }
    // public string PasswordHash { get; set; }
    // public virtual ICollection<Order> Orders { get; set; }
}


// A class to represent a cart
public class Cart
{
    [Key]
    public int CartId { get; set; }
    public int UserId { get; }

    public virtual User User { get; set; }
    public virtual ICollection<CartItem> CartItems { get; set; }
}

// A class to represent a cart item
public class CartItem
{
	[Key]
   	public int CartItemId { get; set; }

   	[Required]
   	public int Quantity {get; set; }

   	public int ProductId {get; set; }
   	public virtual Product Product {get; set;}

   	public int CartId {get; set; }
   	public virtual Cart Cart {get; set;}
}
public class Order
{
    public int OrderId { get; set; }
    [Required]
    public DateTime Date { get; set; }
    public decimal TotalAmount { get; set; }
    // public int CustomerId { get; set; }
    // public Customer Customer { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
}

public class OrderDetail
{
    public int OrderDetailId { get; set;}
    [Required]
    public int Quantity { get; set;}
    public decimal UnitPrice { get; set;}

    public int ProductId { get;set;}
    public virtual Product Product {get;set;}

    public int OrderId {get;set;}
    public virtual Order Order {get;set;}
}