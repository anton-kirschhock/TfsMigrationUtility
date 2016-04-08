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
        IUIManager Manager { get; }
        void InvokePropertyChanges([CallerMemberName]string name="");
        void LoadViewModel();
    }
    public interface IParentViewModel
    {
        Views View { get; }
    }
}
