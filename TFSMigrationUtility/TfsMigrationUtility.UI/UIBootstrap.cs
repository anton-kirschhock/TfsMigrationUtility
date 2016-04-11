using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core;
using TfsMigrationUtility.Core.Bootstrap;
using TfsMigrationUtility.UI.View;
using TfsMigrationUtility.UI.ViewModels;

namespace TfsMigrationUtility.UI
{
    public class UIBootstrap : IBootstrap
    {
        public void Bootstrap()
        {
            //Manager
            ServiceLocator.Set<IUIManager>(new UIManager());
            //MainWindow
            ServiceLocator.Add<IViewModel, MainWindowViewModel>(Views.MainWindow.ToString());
            ServiceLocator.Add<IView, MainWindow>(Views.MainWindow.ToString());

            ServiceLocator.Add<IViewModel, MigrateWindowViewModel>(Views.MigrateWindow.ToString());
            ServiceLocator.Add<IView, MigrateWindows>(Views.MigrateWindow.ToString());
        }
    }
}
