
using Core.Constants;

namespace Core.Request;

public class CreateAccountModel
{
    public string Holder { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    //public decimal Balance { get; set; }
    public string Type { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public int CurrencyId { get; set; }
}
