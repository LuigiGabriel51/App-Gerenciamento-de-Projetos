using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ServiceHub.Model;

namespace ServiceHub.ViewModel
{
    public class VMEditStage : ObservableObject
    {
        private RestService rest;
        public static bool newstage {  get; set; }
        public static bool conclusao { get; set; }
        public static StageModel ProjectStage { get; set; }
        private StageModel stages;
        public StageModel Stages
        {
            get => stages;
            set => SetProperty(ref stages, value);
        }

        private DateTime dateS;
        public DateTime DateS
        {
            get => dateS;
            set => SetProperty(ref dateS, value);
        }

        private string nameS;
        public string NameS
        {
            get => nameS;
            set => SetProperty(ref nameS, value);
        }

        private string descriptionS;
        public string DescriptionS
        {
            get => descriptionS;
            set => SetProperty(ref descriptionS, value);
        }
        public static int idTask;

        private int timeframe;
        public int TimeFrame
        {
            get => timeframe;
            set => SetProperty(ref timeframe, value);
        }

        public IAsyncRelayCommand AddStage {  get; set; }
        public VMEditStage()
        {
            rest = new RestService();
            AddStage = new AsyncRelayCommand(addstage);
            if(ProjectStage == null) Stages = new StageModel();
            else Stages = ProjectStage;
        }

        private async Task addstage()
        {
            if(newstage == false)
            {
                if (nameS == null) nameS = Stages.NameStage;
                if (descriptionS == null) descriptionS = Stages.Description;
                if (TimeFrame == 0) TimeFrame = Stages.TimeFrame;

                StageModel setStage = new StageModel() {
                    Id = Stages.Id,
                    NameStage = NameS,
                    Conclusao = conclusao,
                    DateInit = DateS,
                    Description = descriptionS,
                    Id_Task = ProjectStage.Id_Task,
                    TimeFrame = TimeFrame
                };

                if (await rest.UpdateStage(setStage))
                {
                    Toast toast = new Toast()
                    {
                        Text = "Estágio atualizado!"
                    };
                    await toast.Show();
                }
                else
                {
                    Toast toast = new Toast()
                    {
                        Text = "Falha ao atualizar Estágio!"
                    };
                    await toast.Show();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(NameS))
                {
                    StageModel setStage = new StageModel()
                    {
                        NameStage = NameS,
                        Conclusao = conclusao,
                        DateInit = DateS,
                        Description = descriptionS,
                        Id_Task = idTask,
                        TimeFrame = TimeFrame
                    };
                    if (await rest.AddStage(setStage))
                    {
                        Toast toast = new Toast()
                        {
                            Text = "Novo Estágio adicionado!"
                        };
                        await toast.Show();
                    }
                    else
                    {
                        Toast toast = new Toast()
                        {
                            Text = "Falha ao adicionar Estágio!"
                        };
                        await toast.Show();
                    }
                }
            }
        }
    }
}
