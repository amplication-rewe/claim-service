namespace ClaimService.APIs.Dtos;

public class ReviewCreateInput
{
    public Claim? Claim { get; set; }

    public string? Comments { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public int? Rating { get; set; }

    public string? Remarks { get; set; }

    public DateTime UpdatedAt { get; set; }
}
