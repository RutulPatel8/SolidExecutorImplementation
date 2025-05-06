namespace TPMS.Models
{
    public class Resource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Priority { get; set; }
        public manager ManagedBy { get; set; }
    }
}