using System.ComponentModel.DataAnnotations;

namespace RealEstate.Application.DTOs.PropertyTrace;

public class UpdatePropertyTraceDto
{
    [Required]
    public DateTime DateSale { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Value { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Tax { get; set; }

    [Required]
    public string IdProperty { get; set; } = string.Empty;
}
