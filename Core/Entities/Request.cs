

using Core.Constants;

namespace Core.Entities;

public class Request
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public int? Term { get; set; }
    public string? Brand { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime? ApprovalDate { get; set; }
    public ProductType ProductType { get; set; }
    public SolicitudRequestStatus Status { get; set; } = SolicitudRequestStatus.Pending;
    public int CurrencyId { get; set; }
    public Currency? Currency { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }

}
