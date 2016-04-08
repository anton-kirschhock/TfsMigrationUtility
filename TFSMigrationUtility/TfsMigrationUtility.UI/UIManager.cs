using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core;
using TfsMigrationUtility.UI.View;
using TfsMigrationUtility.UI.ViewModels;

namespace TfsMigrationUtility.UI
{
    public interface IUIManager
    {
        IViewModel GetViewModel(Views view);
        bool ShowView<TViewModel>(TViewModel instance) where TViewModel : IViewModel;
        bool CloseView<TViewModel>(TViewModel instance) where TViewModel : IViewModel;
        IView GetStartupWindow();
    }
    public class UIManager : IUIManager
    {
        /// <summary>
        /// Contains a key,value pair of active windows and its ViewModels
        /// </summary>
        private Dictionary<Type, IView> ViewMapping = new Dictionary<Type, IView>();
        public bool ShowView<TViewModel>(TViewModel instance) where TViewModel : IViewModel
        {
            IView view = null;
            if (!ViewMapping.ContainsKey(typeof(TViewModel)))
            {
                view = ServiceLocator.Get<IView>(instance.View.ToString());//if there is none, get it 
                if (view == null) return false;
                ViewMapping.Add(typeof(TViewModel), view);
            }
            view = ViewMapping[typeof(TViewModel)];
            view.ViewModel = instance;
            view.Show();
            return true;
        }
        public bool CloseView<TViewModel>(TViewModel instance) where TViewModel : IViewModel
        {
            if (ViewMapping.ContainsKey(typeof(TViewModel)))
            {
                IView view = ViewMapping[typeof(TViewModel)];
                if (view == null) return true;
                view.Close();//Releases the resources
                ViewMapping[typeof(TViewModel)] = null;
                ViewMapping.Remove(typeof(TViewModel));//remove it from the active mapping
            }
            return true;
        }
        public IView GetStartupWindow()
        {
            IView view = null;
            if (!ViewMapping.ContainsKey(typeof(MainWindowViewModel)))
            {
                view = ServiceLocator.Get<IView>(Views.MainWindow.ToString());//if there is none, get it 
                if (view == null) throw new Exception("Cannot locate the startup view!");
                ViewMapping.Add(typeof(MainWindowViewModel), view);
            }
            view = ViewMapping[typeof(MainWindowViewModel)];
            IViewModel vm = GetViewModel(Views.MainWindow);
            return view;
        }

        public IViewModel GetViewModel(Views view)
        {
            return ServiceLocator.Get<IViewModel>(view.ToString());
        }
    }
}
