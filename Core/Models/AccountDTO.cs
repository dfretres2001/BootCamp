

using Core.Constants;
using Core.Entities;

namespace Core.Models;

public class AccountDTO
{
    public int Id { get; set; }
    public string Holder { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public AccountType Type { get; set; } = AccountType.Current;
    public decimal Balance { get; set; }
    public string Status { get; set; } = string.Empty;
    //public IsDeletedStatus IsDeleted { get; set; } = IsDeletedStatus.False;
    //public int CurrencyId { get; set; } //ojo
    public CurrencyDTO Currency { get; set; } = null!;
    //public int CustomerId { get; set; } //ojo
    public CustomerDTO Customer { get; set; } = null!;
    public SavingAccountDTO? SavingAccount { get; set; } 
    public CurrentAccountDTO? CurrentAccount { get; set; } //OJO
}
