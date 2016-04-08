using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TfsMigrationUtility.Core;
using TfsMigrationUtility.UI.ViewModels;

namespace TfsMigrationUtility.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            MainWindow = ServiceLocator.Get<IUIManager>().GetStartupWindow() as Window;
            ShutdownMode = ShutdownMode.OnMainWindowClose;
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow.ShowDialog();
        }

    }
}
