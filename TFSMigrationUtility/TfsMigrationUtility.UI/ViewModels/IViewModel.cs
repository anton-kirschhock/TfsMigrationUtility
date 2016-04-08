using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TfsMigrationUtility.UI.ViewModels
{
    public enum Views
    {
        MainWindow
    };

    public interface IViewModel:INotifyPropertyChanged,IDisposable
    {
        Views View { get; }
        UIManager Manager { get; }
        void InvokePropertyChanges([CallerMemberName]string name="");
        void LoadViewModel();
    }
}
