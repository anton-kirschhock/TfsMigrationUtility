using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core;

namespace TfsMigrationUtility.UI.ViewModels
{
    /// <summary>
    /// A generic wrapper to assist with the Propertychanged, containing a Value of type T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICell<T>:INotifyPropertyChanged
    {
        T Value { get; set; }
        bool HasValue();
    }
    /// <summary>
    /// A generic wrapper to assist with the Propertychanged. Because they are "little viewmodels", they inherit of AbstractNestedViewModel, which can be nested.
    /// Only Important is the interface and Cell Class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Cell<T> : AbstractNestedViewModel, ICell<T>
    {
        private T _value;
        public T Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
                InvokePropertyChanges();
            }
        }
        public Cell(T value,IViewModel parent,string propertyname):base(parent,propertyname)
        {
            _value = value;
        }

        public bool HasValue()
        {
            return Value != null;
        }
    }
}
