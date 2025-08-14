using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs.PropertyImage;
using RealEstate.Application.Services;

namespace RealEstate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyImagesController : ControllerBase
{
    private readonly IPropertyImageService _propertyImageService;

    public PropertyImagesController(IPropertyImageService propertyImageService)
    {
        _propertyImageService = propertyImageService;
    }

    /// <summary>
    /// Get all property images
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropertyImageDto>>> GetAll()
    {
        try
        {
            var propertyImages = await _propertyImageService.GetAllAsync();
            return Ok(propertyImages);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

    /// <summary>
    /// Get property image by id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<PropertyImageDto>> GetById(string id)
    {
        try
        {
            var propertyImage = await _propertyImageService.GetByIdAsync(id);
            if (propertyImage == null)
                return NotFound(new { message = "Property image not found" });

            return Ok(propertyImage);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

    /// <summary>
    /// Get property images by property id
    /// </summary>
    [HttpGet("property/{propertyId}")]
    public async Task<ActionResult<IEnumerable<PropertyImageDto>>> GetByPropertyId(
        string propertyId
    )
    {
        try
        {
            var propertyImages = await _propertyImageService.GetByPropertyIdAsync(propertyId);
            return Ok(propertyImages);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

    /// <summary>
    /// Create a new property image
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PropertyImageDto>> Create(
        [FromBody] CreatePropertyImageDto createPropertyImageDto
    )
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var propertyImage = await _propertyImageService.CreateAsync(createPropertyImageDto);
            return CreatedAtAction(nameof(GetById), new { id = propertyImage.Id }, propertyImage);
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
    /// Update an existing property image
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<PropertyImageDto>> Update(
        string id,
        [FromBody] UpdatePropertyImageDto updatePropertyImageDto
    )
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var propertyImage = await _propertyImageService.UpdateAsync(id, updatePropertyImageDto);
            return Ok(propertyImage);
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
    /// Delete a property image
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            var result = await _propertyImageService.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = "Property image not found" });

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }
}
