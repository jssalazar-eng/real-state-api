namespace RealEstate.Application.DTOs.PropertyTrace;

public class PropertyTraceDto
{
    public string Id { get; set; } = string.Empty;
    public string IdPropertyTrace { get; set; } = string.Empty;
    public DateTime DateSale { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public decimal Tax { get; set; }
    public string IdProperty { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
