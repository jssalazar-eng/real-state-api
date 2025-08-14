using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs.Property;
using RealEstate.Application.Services;

namespace RealEstate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertiesController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    /// <summary>
    /// Get all properties
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropertyDto>>> GetAll()
    {
        try
        {
            var properties = await _propertyService.GetAllAsync();
            return Ok(properties);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

    /// <summary>
    /// Get property by id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<PropertyDto>> GetById(string id)
    {
        try
        {
            var property = await _propertyService.GetByIdAsync(id);
            if (property == null)
                return NotFound(new { message = "Property not found" });

            return Ok(property);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

    /// <summary>
    /// Get properties by owner id
    /// </summary>
    [HttpGet("owner/{ownerId}")]
    public async Task<ActionResult<IEnumerable<PropertyDto>>> GetByOwnerId(string ownerId)
    {
        try
        {
            var properties = await _propertyService.GetByOwnerIdAsync(ownerId);
            return Ok(properties);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

    /// <summary>
    /// Create a new property
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PropertyDto>> Create(
        [FromBody] CreatePropertyDto createPropertyDto
    )
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var property = await _propertyService.CreateAsync(createPropertyDto);
            return CreatedAtAction(nameof(GetById), new { id = property.Id }, property);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

    /// <summary>
    /// Update an existing property
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<PropertyDto>> Update(
        string id,
        [FromBody] UpdatePropertyDto updatePropertyDto
    )
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var property = await _propertyService.UpdateAsync(id, updatePropertyDto);
            return Ok(property);
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

    /// <summary>
    /// Delete a property
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            var result = await _propertyService.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = "Property not found" });

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }
}
