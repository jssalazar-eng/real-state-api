using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs.Owner;
using RealEstate.Application.Services;

namespace RealEstate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OwnersController : ControllerBase
{
    private readonly IOwnerService _ownerService;

    public OwnersController(IOwnerService ownerService)
    {
        _ownerService = ownerService;
    }

    /// <summary>
    /// Get all owners
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OwnerDto>>> GetAll()
    {
        try
        {
            var owners = await _ownerService.GetAllAsync();
            return Ok(owners);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

    /// <summary>
    /// Get owner by id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<OwnerDto>> GetById(string id)
    {
        try
        {
            var owner = await _ownerService.GetByIdAsync(id);
            if (owner == null)
                return NotFound(new { message = "Owner not found" });

            return Ok(owner);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

    /// <summary>
    /// Create a new owner
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<OwnerDto>> Create([FromBody] CreateOwnerDto createOwnerDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var owner = await _ownerService.CreateAsync(createOwnerDto);
            return CreatedAtAction(nameof(GetById), new { id = owner.Id }, owner);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

    /// <summary>
    /// Update an existing owner
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<OwnerDto>> Update(
        string id,
        [FromBody] UpdateOwnerDto updateOwnerDto
    )
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var owner = await _ownerService.UpdateAsync(id, updateOwnerDto);
            return Ok(owner);
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
    /// Delete an owner
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            var result = await _ownerService.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = "Owner not found" });

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }
}
