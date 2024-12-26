using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClaimService.Infrastructure.Models;

[Table("Reviews")]
public class ReviewDbModel
{
    public string? ClaimId { get; set; }

    [ForeignKey(nameof(ClaimId))]
    public ClaimDbModel? Claim { get; set; } = null;

    [StringLength(1000)]
    public string? Comments { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [Range(-999999999, 999999999)]
    public int? Rating { get; set; }

    [StringLength(1000)]
    public string? Remarks { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
