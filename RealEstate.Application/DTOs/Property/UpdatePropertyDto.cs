using System.ComponentModel.DataAnnotations;

namespace RealEstate.Application.DTOs.Property;

public class UpdatePropertyDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Address { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    [StringLength(50)]
    public string CodeInternal { get; set; } = string.Empty;

    [Required]
    [Range(1800, 2100)]
    public int Year { get; set; }

    [Required]
    public string IdOwner { get; set; } = string.Empty;
}
