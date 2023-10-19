using ServiceHub.Model;

namespace ServiceHubAPI.Model
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string NameProject { get; set; }
        public string Users { get; set; }
        public byte[] Image {  get; set; } 
    }
    public class StageModel
    {
        public int Id { get; set; }
        public int Id_Task { get; set; }
        public string NameStage { get; set; }
        public string Description { get; set; }
        public DateTime DateInit { get; set; }
        public int TimeFrame { get; set; }
        public bool Conclusao { get; set; }
    }
}
