namespace TPMS.Models
{
    public class tpms_task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TaskType { get; set; }
        public bool IsActive { get; set; }
        public manager ManagedBy { get; set; }
    }
}