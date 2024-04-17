
namespace Core.Request;

public class UpdatePromotionModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int Discount { get; set; }
    public List<int> RelatedEnterpriseIds { get; set; } = new List<int>();
}
