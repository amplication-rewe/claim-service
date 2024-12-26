using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClaimService.Infrastructure.Models;

[Table("Customers")]
public class CustomerDbModel
{
    public List<ClaimDbModel>? Claims { get; set; } = new List<ClaimDbModel>();

    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    [StringLength(1000)]
    public string? FirstName { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? LastName { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
