using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsMigrationUtility.UI.View.Factories
{
    public class MainWindowFactory : IViewFactory
    {
        public IView BuildNewView()
        {
            return new MainWindow();
        }       
    }
}
