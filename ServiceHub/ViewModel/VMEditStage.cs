using CommunityToolkit.Mvvm.ComponentModel;
using ServiceHub.Model;

namespace ServiceHub.ViewModel
{
    public class VMEditStage : ObservableObject
    {
        public static bool conclusao { get; set; }
        public static StageModel ProjectStage { get; set; }
        private StageModel stages;
        public StageModel Stages
        {
            get => stages;
            set => SetProperty(ref stages, value);
        }
        private StageModel newstages;
        public StageModel NewStages
        {
            get => newstages;
            set => SetProperty(ref newstages, value);
        }
        public VMEditStage()
        {
            Stages = ProjectStage;
        }

    }
}
