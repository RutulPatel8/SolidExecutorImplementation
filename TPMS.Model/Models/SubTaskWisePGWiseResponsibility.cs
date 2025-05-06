// File: TPMS.LTVL/ResponsibilityExecutor.cs
namespace TPMS.Models
{
    public class SubTaskWisePGWiseResponsibility
    {
        public int Id { get; set; }
        public string SubTaskExternalUniqueId { get; set; }
        public Resource Resource { get; set; }
        public manager ManagedBy { get; set; }
    }
}