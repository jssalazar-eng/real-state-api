namespace RealEstate.Domain.Entities;

public class Property
{
    public string Id { get; set; } = string.Empty;
    public string IdProperty { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string CodeInternal { get; set; } = string.Empty;
    public int Year { get; set; }
    public string IdOwner { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public Owner? Owner { get; set; }
    public List<PropertyImage> PropertyImages { get; set; } = new();
    public List<PropertyTrace> PropertyTraces { get; set; } = new();
}
