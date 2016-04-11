using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsMigrationUtility.UI.View.Factories
{
    public interface IViewFactory
    {
        IView BuildNewView();
    }
}
