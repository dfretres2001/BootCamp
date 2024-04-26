

using Core.Constants;

namespace Core.Request;

public class CreateTransferModel
{
    public int OriginAccountId { get; set; }
    public TransactionType ConceptType { get; set; } 
    public int DenstinationBankId { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public string DocumentNumber {  get; set; } = string.Empty;
    public int CurrencyId { get; set; }
    public decimal Amount { get; set; }
    public string Concept { get; set; } = string.Empty;
}