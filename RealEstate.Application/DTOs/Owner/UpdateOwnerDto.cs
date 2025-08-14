using System.ComponentModel.DataAnnotations;

namespace RealEstate.Application.DTOs.Owner;

public class UpdateOwnerDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Address { get; set; } = string.Empty;

    public string? Photo { get; set; }

    [Required]
    public DateTime Birthday { get; set; }
}
