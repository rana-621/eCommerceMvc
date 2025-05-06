using System.ComponentModel.DataAnnotations;

namespace eCommerceMvc.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    [Required(ErrorMessage = "Category name is required")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<Product> Products { get; set; }
}
