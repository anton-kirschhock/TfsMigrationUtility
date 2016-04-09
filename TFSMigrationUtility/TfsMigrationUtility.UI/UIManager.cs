﻿using System;
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
        bool ShowView<TViewModel>(TViewModel instance) where TViewModel : IViewModel, IParentViewModel;
        bool CloseView<TViewModel>(TViewModel instance) where TViewModel : IViewModel, IParentViewModel;
        IView GetStartupWindow();
    }
    public class UIManager : IUIManager
    {
        /// <summary>
        /// Contains a key,value pair of active windows and its ViewModels
        /// </summary>
        private Dictionary<string, IView> ViewMapping = new Dictionary<string, IView>();
        public bool ShowView<TViewModel>(TViewModel instance) where TViewModel : IViewModel,IParentViewModel
        {
            IView view = null;
            if (!ViewMapping.ContainsKey(instance.View.ToString()))
            {
                view = ServiceLocator.Get<IView>(instance.View.ToString());//if there is none, get it 
                if (view == null) return false;
                ViewMapping.Add(instance.View.ToString(), view);
            }
            view = ViewMapping[instance.View.ToString()];
            view.ViewModel = instance;
            view.ShowView();
            return true;
        }
        public bool CloseView<TViewModel>(TViewModel instance) where TViewModel : IViewModel, IParentViewModel
        {
            if (ViewMapping.ContainsKey(instance.View.ToString()))
            {
                IView view = ViewMapping[instance.View.ToString()];
                if (view == null) return true;
                view.Close();//Releases the resources
                ViewMapping[instance.View.ToString()] = null;
                ViewMapping.Remove(instance.View.ToString());//remove it from the active mapping
            }
            return true;
        }
        public IView GetStartupWindow()
        {
            IView view = null;
            if (!ViewMapping.ContainsKey(Views.MainWindow.ToString()))
            {
                view = ServiceLocator.Get<IView>(Views.MainWindow.ToString());//if there is none, get it 
                if (view == null) throw new Exception("Cannot locate the startup view!");
                ViewMapping.Add(Views.MainWindow.ToString(), view);
            }
            view = ViewMapping[Views.MainWindow.ToString()];
            IViewModel vm = GetViewModel(Views.MainWindow);
            view.ViewModel = vm;
            return view;
        }

        public IViewModel GetViewModel(Views view)
        {
            return ServiceLocator.Get<IViewModel>(view.ToString());
        }
    }
}
