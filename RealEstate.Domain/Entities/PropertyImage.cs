namespace RealEstate.Domain.Entities;

public class PropertyImage
{
    public string Id { get; set; } = string.Empty;
    public string IdPropertyImage { get; set; } = string.Empty;
    public string IdProperty { get; set; } = string.Empty;
    public string File { get; set; } = string.Empty;
    public bool Enabled { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public Property? Property { get; set; }
}
