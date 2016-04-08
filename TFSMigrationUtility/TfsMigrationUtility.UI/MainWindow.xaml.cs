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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TfsMigrationUtility.UI.View;
using TfsMigrationUtility.UI.ViewModels;

namespace TfsMigrationUtility.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }
        public IViewModel ViewModel
        {
            get
            {
                return this.DataContext as IViewModel;
            }

            set
            {
                this.DataContext = value;
            }
        }
    }
}
