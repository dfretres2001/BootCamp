

using Core.Constants;

namespace Core.Models;
public class TransactionDTO
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public string Concept { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransferredDateTime { get; set; }
    public int AccountId { get; set; }
    public int? CurrencyId { get; set; } 
}
