// File: TPMS.LTVL/ResponsibilityExecutor.cs
namespace TPMS.Models
{
    public class StageWisePGWiseResponsibility
    {
        public int Id { get; set; }
        public string TaskExternalUniqueId { get; set; }
        public Resource Resource { get; set; }
        public manager ManagedBy { get; set; }
    }
}