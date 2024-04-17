

using Core.Constants;

namespace Core.Models;

public class RequestDTO
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public int? Term { get; set; }
    public string? Brand { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime? ApprovalDate { get; set; }
    public ProductType ProductType { get; set; }
    public SolicitudRequestStatus Status { get; set; } = SolicitudRequestStatus.Pending;
    public CurrencyDTO Currency { get; set; } = null!;
    public CustomerDTO Customer { get; set; } = null!;

}
