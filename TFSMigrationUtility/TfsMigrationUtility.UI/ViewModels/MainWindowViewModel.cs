using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsMigrationUtility.UI.ViewModels
{
    public class MainWindowViewModel : AbstractViewModel
    {
//PROPERTIES
        public override Views View { get { return Views.MainWindow; } }
    //VIEWMODELPROPERTIES
        
//CTOR
        public MainWindowViewModel(UIManager uimanager):base(uimanager){
            LoadViewModel();
        }

//METHODS
        public override void LoadViewModel()
        {
            //Prepare stuff here
            base.LoadViewModel();
        }
    }
}
