using ClaimService.APIs.Common;
using ClaimService.APIs.Dtos;

namespace ClaimService.APIs;

public interface IClaimsService
{
    /// <summary>
    /// Create one Claim
    /// </summary>
    public Task<Claim> CreateClaim(ClaimCreateInput claim);

    /// <summary>
    /// Delete one Claim
    /// </summary>
    public Task DeleteClaim(ClaimWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Claims
    /// </summary>
    public Task<List<Claim>> Claims(ClaimFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Claim records
    /// </summary>
    public Task<MetadataDto> ClaimsMeta(ClaimFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Claim
    /// </summary>
    public Task<Claim> Claim(ClaimWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Claim
    /// </summary>
    public Task UpdateClaim(ClaimWhereUniqueInput uniqueId, ClaimUpdateInput updateDto);

    /// <summary>
    /// Get a Customer record for Claim
    /// </summary>
    public Task<Customer> GetCustomer(ClaimWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple Reviews records to Claim
    /// </summary>
    public Task ConnectReviews(ClaimWhereUniqueInput uniqueId, ReviewWhereUniqueInput[] reviewsId);

    /// <summary>
    /// Disconnect multiple Reviews records from Claim
    /// </summary>
    public Task DisconnectReviews(
        ClaimWhereUniqueInput uniqueId,
        ReviewWhereUniqueInput[] reviewsId
    );

    /// <summary>
    /// Find multiple Reviews records for Claim
    /// </summary>
    public Task<List<Review>> FindReviews(
        ClaimWhereUniqueInput uniqueId,
        ReviewFindManyArgs ReviewFindManyArgs
    );

    /// <summary>
    /// Update multiple Reviews records for Claim
    /// </summary>
    public Task UpdateReviews(ClaimWhereUniqueInput uniqueId, ReviewWhereUniqueInput[] reviewsId);
}
