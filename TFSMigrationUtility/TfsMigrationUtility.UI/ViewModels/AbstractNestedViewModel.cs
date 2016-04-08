using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core;

namespace TfsMigrationUtility.UI.ViewModels
{
    /// <summary>
    /// A viewmodel which can be within a viewmodel. This will trigger "PropertyChanged" of the parent.
    /// </summary>
    public abstract class AbstractNestedViewModel : AbstractViewModel
    {
        protected IViewModel Parent { get; private set; }
        private string PropertyName { get; set; }

        public AbstractNestedViewModel(IViewModel Parent,string PropertyName):base()
        {
            this.Parent = Parent;
            this.PropertyName = PropertyName;
            this.PropertyChanged += (s, e)=> Parent?.InvokePropertyChanges(PropertyName);
        }
        public override void LoadViewModel(){}
    }
}
