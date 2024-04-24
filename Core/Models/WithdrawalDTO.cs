

using Core.Entities;

namespace Core.Models;

public class WithdrawalDTO
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DepositDateTime { get; set; }
    public int AccountId { get; set; }
    //public int BankId { get; set; }
    public string Account { get; set; } = string.Empty; //ojo
    //public Bank Bank { get; set; } = null!;
}
