// File: TPMS.LTVL/ResponsibilityExecutor.cs
namespace TPMS.Models
{
    public class organization_structure
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ExternalUniqueId { get; set; }
        public manager ManagedBy { get; set; }
    }
}