

using Core.Constants;

namespace Core.Entities;

public class Product
{
    public int Id { get; set; }
    public ProductType ProductType { get; set; }
    //public decimal Amount { get; set; } IR A SOLI
    //public int? Term { get; set; }   IR A SOLI
    //public string? Brand { get; set; }
    //public string Currency { get; set; } IR A SOLI
    //public DateTime RequestDate { get; set; } IR A SOLI
    //public DateTime? ApprovalDate { get; set; } IR A SOLI
    //public ProductRequestStatus Status { get; set; } = ProductRequestStatus.Pending;
    public ICollection<Request> Requests { get; set; } = new List<Request>();

}
