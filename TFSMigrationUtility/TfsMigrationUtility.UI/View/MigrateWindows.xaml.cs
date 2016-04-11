using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TfsMigrationUtility.UI.ViewModels;

namespace TfsMigrationUtility.UI.View
{
    /// <summary>
    /// Interaction logic for MigrateWindows.xaml
    /// </summary>
    public partial class MigrateWindows : Window,IView
    {
        public MigrateWindows()
        {
            InitializeComponent();
        }

        public IViewModel ViewModel
        {
            get
            {
                return DataContext as IViewModel;
            }

            set
            {
                DataContext = value;
            }
        }

        public void ShowView()
        {
            Show();
           
        }
        
    }
}

