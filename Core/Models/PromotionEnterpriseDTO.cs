

namespace Core.Models;

public class PromotionEnterpriseDTO
{
    public int EnterpriseId { get; set; }
    public EnterpriseDTO Enterprise { get; set; }
    public PromotionDTO Promotion { get; set; }
}
