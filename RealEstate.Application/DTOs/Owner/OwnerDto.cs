namespace RealEstate.Application.DTOs.Owner;

public class OwnerDto
{
    public string Id { get; set; } = string.Empty;
    public string IdOwner { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Photo { get; set; }
    public DateTime Birthday { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
