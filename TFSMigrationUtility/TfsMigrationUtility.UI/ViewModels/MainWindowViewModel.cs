using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TfsMigrationUtility.UI.ViewModels
{
    public class MainWindowViewModel : AbstractParentViewModel
    {
//PROPERTIES
        public override Views View { get { return Views.MainWindow; } }
    //VIEWMODELPROPERTIES
        public string AppVersion
        {
            get
            {
                return "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
//CTOR
        public MainWindowViewModel(UIManager uimanager):base(uimanager){
            LoadViewModel();
        }

//METHODS
        public override void LoadViewModel()
        {
            //Prepare stuff here
            //base.LoadViewModel();
        }
    }
}
