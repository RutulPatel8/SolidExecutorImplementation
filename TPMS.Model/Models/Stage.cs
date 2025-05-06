namespace TPMS.Models
{
    public class Stage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ExternalUniqueId { get; set; }
        public double Sequence { get; set; }
        public string SuccessorStage { get; set; }
    }
}