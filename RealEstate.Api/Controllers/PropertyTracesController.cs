using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs.PropertyTrace;
using RealEstate.Application.Services;

namespace RealEstate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyTracesController : ControllerBase
{
    private readonly IPropertyTraceService _propertyTraceService;

    public PropertyTracesController(IPropertyTraceService propertyTraceService)
    {
        _propertyTraceService = propertyTraceService;
    }

    /// <summary>
    /// Get all property traces
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropertyTraceDto>>> GetAll()
    {
        try
        {
            var propertyTraces = await _propertyTraceService.GetAllAsync();
            return Ok(propertyTraces);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

    /// <summary>
    /// Get property trace by id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<PropertyTraceDto>> GetById(string id)
    {
        try
        {
            var propertyTrace = await _propertyTraceService.GetByIdAsync(id);
            if (propertyTrace == null)
                return NotFound(new { message = "Property trace not found" });

            return Ok(propertyTrace);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

    /// <summary>
    /// Get property traces by property id
    /// </summary>
    [HttpGet("property/{propertyId}")]
    public async Task<ActionResult<IEnumerable<PropertyTraceDto>>> GetByPropertyId(
        string propertyId
    )
    {
        try
        {
            var propertyTraces = await _propertyTraceService.GetByPropertyIdAsync(propertyId);
            return Ok(propertyTraces);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

    /// <summary>
    /// Create a new property trace
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PropertyTraceDto>> Create(
        [FromBody] CreatePropertyTraceDto createPropertyTraceDto
    )
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var propertyTrace = await _propertyTraceService.CreateAsync(createPropertyTraceDto);
            return CreatedAtAction(nameof(GetById), new { id = propertyTrace.Id }, propertyTrace);
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
    /// Update an existing property trace
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<PropertyTraceDto>> Update(
        string id,
        [FromBody] UpdatePropertyTraceDto updatePropertyTraceDto
    )
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var propertyTrace = await _propertyTraceService.UpdateAsync(id, updatePropertyTraceDto);
            return Ok(propertyTrace);
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
    /// Delete a property trace
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            var result = await _propertyTraceService.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = "Property trace not found" });

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }
}
