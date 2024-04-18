

using Core.Constants;

namespace Core.Entities;

public class Product
{
    public int Id { get; set; }
    public string ProductType { get; set; } 
    public ICollection<Request> Requests { get; set; } = new List<Request>();
}
