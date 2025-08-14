using System.ComponentModel.DataAnnotations;

namespace RealEstate.Application.DTOs.PropertyImage;

public class CreatePropertyImageDto
{
    [Required]
    public string IdProperty { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string File { get; set; } = string.Empty;

    public bool Enabled { get; set; } = true;
}
