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
    public interface ICell<T> : INotifyPropertyChanged
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
        public Cell(T value, IViewModel parent, string propertyname) : base(parent, propertyname)
        {
            _value = value;
        }
        public Cell(IViewModel parent, string propertyname) : this(default(T), parent, propertyname) { }

        public virtual bool HasValue()
        {
            return Value != null;
        }
    }
    public interface IStringCell : ICell<string>
    {
        /// <summary>
        /// Appends the line + \n
        /// </summary>
        /// <param name="line">Line/Text to append</param>
        void AppendLine(string line);
        /// <summary>
        /// Simply appends the line without \n
        /// </summary>
        /// <param name="line">Line/Text to append</param>
        void Append(string line);
    }
    public class StringCell : Cell<string>,IStringCell
    {
        public StringCell(IViewModel parent, string propertyname) : this("",parent, propertyname)
        {
        }

        public StringCell(string value, IViewModel parent, string propertyname) : base(value, parent, propertyname)
        {
        }
        public void AppendLine(string line)
        {
            Append(line + "\n");
        }

        public void Append(string line)
        {
            this.Value += line;
        }
        public override bool HasValue()
        {
            return !string.IsNullOrEmpty(Value);
        }
    }
}
