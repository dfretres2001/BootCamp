

using Core.Constants;
using Core.Entities;

namespace Core.Request;

public class UpdateAccountModel
{
    public int Id { get; set; }
    public string Holder { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public AccountStatus Status { get; set; } = AccountStatus.Active;
    public int CurrencyId { get; set; }
    public int CustomerId { get; set; }
}
