using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TfsMigrationUtility.UI.ViewModels
{
    /// <summary>
    /// Abstract base implementation of a viewmodel
    /// </summary>
    public abstract class AbstractViewModel : IViewModel
    {
        /// <summary>
        /// Event indicating if a property was changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The Active UIManager. Can also be resolved via DependancyInjection
        /// </summary>
        public IUIManager Manager { get; private set; }

        public AbstractViewModel(IUIManager manager)
        {
            Manager = manager;
        }
        /// <summary>
        /// Disposes the Viewmodel and calles the manager to close the view
        /// </summary>
        public void Dispose()
        {
            Manager.CloseView(this);
        }
        /// <summary>
        /// Invokes the propertychanged and uses the CallerMemberName to trigger it
        /// </summary>
        /// <param name="name">OPTIONAL: the CallerMemberName</param>
        public void InvokePropertyChanges([CallerMemberName] string name = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        /// <summary>
        /// Loads additional properties and dependancies of this viewmodel. First load everything then call the base.LoadViewModel() to finish
        /// </summary>
        public virtual void LoadViewModel()
        {
            Manager.ShowView(this);
        }
    }
    public abstract class AbstractParentViewModel : AbstractViewModel, IParentViewModel
    {
        public AbstractParentViewModel(UIManager UIManager) :base(UIManager) {}
        /// <summary>
        /// The View type of this ViewModel
        /// </summary>
        public abstract Views View { get; }
    }
}
