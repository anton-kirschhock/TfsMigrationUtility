using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core;
using TfsMigrationUtility.Core.Bootstrap;
using TfsMigrationUtility.Core.Progress;
using TfsMigrationUtility.UI.View;
using TfsMigrationUtility.UI.View.Factories;
using TfsMigrationUtility.UI.ViewModels;
using TfsMigrationUtility.UI.ViewModels.Nested;

namespace TfsMigrationUtility.UI
{
    public class UIBootstrap : IBootstrap
    {
        public void Bootstrap()
        {
            //Manager
            ServiceLocator.Set<IUIManager>(new UIManager());
            //MainWindow
            ServiceLocator.Set<IViewModel, MainWindowViewModel>(Views.MainWindow.ToString(),true);
            ServiceLocator.Set<IViewFactory, MainWindowFactory>(Views.MainWindow.ToString());

            ServiceLocator.Set<IViewModel, MigrateWindowViewModel>(Views.MigrateWindow.ToString(), true);
            ServiceLocator.Set<IViewFactory, MigrateWindowFactory>(Views.MigrateWindow.ToString());
        }
    }
}
