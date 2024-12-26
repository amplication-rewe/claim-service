using ClaimService.APIs.Dtos;
using ClaimService.Infrastructure.Models;

namespace ClaimService.APIs.Extensions;

public static class ReviewsExtensions
{
    public static Review ToDto(this ReviewDbModel model)
    {
        return new Review
        {
            Claim = model.ClaimId,
            Comments = model.Comments,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Rating = model.Rating,
            Remarks = model.Remarks,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ReviewDbModel ToModel(
        this ReviewUpdateInput updateDto,
        ReviewWhereUniqueInput uniqueId
    )
    {
        var review = new ReviewDbModel
        {
            Id = uniqueId.Id,
            Comments = updateDto.Comments,
            Rating = updateDto.Rating,
            Remarks = updateDto.Remarks
        };

        if (updateDto.Claim != null)
        {
            review.ClaimId = updateDto.Claim;
        }
        if (updateDto.CreatedAt != null)
        {
            review.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            review.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return review;
    }
}
