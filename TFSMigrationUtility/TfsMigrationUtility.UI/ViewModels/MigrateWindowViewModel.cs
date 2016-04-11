using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TfsMigrationUtility.Core;
using TfsMigrationUtility.Core.Progress;
using TfsMigrationUtility.UI.ViewModels.Nested;
using TfsMigrationUtility.UI.Workers;

namespace TfsMigrationUtility.UI.ViewModels
{
    public class MigrateWindowViewModel : AbstractParentViewModel
    {
        public override Views View
        {
            get
            {
                return Views.MigrateWindow;
            }
        }
        public bool IsRunning { get
            {
                return Worker.IsRunning;
            }
        }
        public MigrationWorker Worker { get; set; }

        public ProgressViewModel ProgressViewModel { get; set; }
        public ICommand OnBack
        {
            get
            {
                return new RelayCommand(
                    _ =>
                    {
                        Manager.CloseView(this);
                    }, _ => !IsRunning);
            }
        }

        public IConfigurationViewModel ConfigVM { get; set; }
        public MigrateWindowViewModel(IUIManager manager):base(manager)
        {
            LoadViewModel();
        }

        public override void LoadViewModel()
        {
            Worker = new MigrationWorker();
            ProgressViewModel = new ProgressViewModel(this, nameof(ProgressViewModel));
            ServiceLocator.Set<IProgress>(ProgressViewModel);
            base.LoadViewModel();
        }

        public void LoadConfig(IConfigurationViewModel config)
        {
            ConfigVM = config;
            
            Task.Run(()=>this.Worker.Start(config, this.ProgressViewModel));//start the worker async, so the UI stays untouched
        }

    }
}
