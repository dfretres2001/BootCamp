
using Core.Constants;

namespace Core.Request;

public class FilterTransactionModel
{
    public int AccountId { get; set; }
    public int? Month { get; set; }
    public int? Year { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public TransactionType ConceptType { get; set; } = TransactionType.All;
}