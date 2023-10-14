using CommunityToolkit.Mvvm.ComponentModel;
using ServiceHub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.ViewModel
{
    public class VMEditStage: ObservableObject
    {
        public static bool conclusao {  get; set; }
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
