using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceMvc.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Product name is required")]
    [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Product price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Product price must be greater than 0")]
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }

    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }
}
