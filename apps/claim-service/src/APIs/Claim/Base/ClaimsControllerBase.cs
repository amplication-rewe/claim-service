using ClaimService.APIs;
using ClaimService.APIs.Common;
using ClaimService.APIs.Dtos;
using ClaimService.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ClaimsControllerBase : ControllerBase
{
    protected readonly IClaimsService _service;

    public ClaimsControllerBase(IClaimsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Claim
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Claim>> CreateClaim(ClaimCreateInput input)
    {
        var claim = await _service.CreateClaim(input);

        return CreatedAtAction(nameof(Claim), new { id = claim.Id }, claim);
    }

    /// <summary>
    /// Delete one Claim
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteClaim([FromRoute()] ClaimWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteClaim(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Claims
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Claim>>> Claims([FromQuery()] ClaimFindManyArgs filter)
    {
        return Ok(await _service.Claims(filter));
    }

    /// <summary>
    /// Meta data about Claim records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ClaimsMeta([FromQuery()] ClaimFindManyArgs filter)
    {
        return Ok(await _service.ClaimsMeta(filter));
    }

    /// <summary>
    /// Get one Claim
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Claim>> Claim([FromRoute()] ClaimWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Claim(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Claim
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateClaim(
        [FromRoute()] ClaimWhereUniqueInput uniqueId,
        [FromQuery()] ClaimUpdateInput claimUpdateDto
    )
    {
        try
        {
            await _service.UpdateClaim(uniqueId, claimUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Customer record for Claim
    /// </summary>
    [HttpGet("{Id}/customer")]
    public async Task<ActionResult<List<Customer>>> GetCustomer(
        [FromRoute()] ClaimWhereUniqueInput uniqueId
    )
    {
        var customer = await _service.GetCustomer(uniqueId);
        return Ok(customer);
    }

    /// <summary>
    /// Connect multiple Reviews records to Claim
    /// </summary>
    [HttpPost("{Id}/reviews")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectReviews(
        [FromRoute()] ClaimWhereUniqueInput uniqueId,
        [FromQuery()] ReviewWhereUniqueInput[] reviewsId
    )
    {
        try
        {
            await _service.ConnectReviews(uniqueId, reviewsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Reviews records from Claim
    /// </summary>
    [HttpDelete("{Id}/reviews")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectReviews(
        [FromRoute()] ClaimWhereUniqueInput uniqueId,
        [FromBody()] ReviewWhereUniqueInput[] reviewsId
    )
    {
        try
        {
            await _service.DisconnectReviews(uniqueId, reviewsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Reviews records for Claim
    /// </summary>
    [HttpGet("{Id}/reviews")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Review>>> FindReviews(
        [FromRoute()] ClaimWhereUniqueInput uniqueId,
        [FromQuery()] ReviewFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindReviews(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Reviews records for Claim
    /// </summary>
    [HttpPatch("{Id}/reviews")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateReviews(
        [FromRoute()] ClaimWhereUniqueInput uniqueId,
        [FromBody()] ReviewWhereUniqueInput[] reviewsId
    )
    {
        try
        {
            await _service.UpdateReviews(uniqueId, reviewsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
