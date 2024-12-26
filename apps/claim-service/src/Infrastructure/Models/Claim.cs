using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClaimService.Infrastructure.Models;

[Table("Claims")]
public class ClaimDbModel
{
    [Range(-999999999, 999999999)]
    public double? ClaimAmount { get; set; }

    public DateTime? ClaimDate { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public CustomerDbModel? Customer { get; set; } = null;

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? PolicyNumber { get; set; }

    public List<ReviewDbModel>? Reviews { get; set; } = new List<ReviewDbModel>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
