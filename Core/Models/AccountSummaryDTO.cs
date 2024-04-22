
namespace Core.Models;
public class AccountSummaryDTO
{
    public string Holder { get; set; }
    public decimal Balance { get; set; }
    public CurrencyDTO Currency { get; set; }
    public CustomerDTO Customer { get; set; }
}
