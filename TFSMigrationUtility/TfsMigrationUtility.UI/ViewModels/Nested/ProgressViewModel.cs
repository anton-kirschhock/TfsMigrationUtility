using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TfsMigrationUtility.Core.Progress;

namespace TfsMigrationUtility.UI.ViewModels.Nested
{
    public interface IProgressViewModel: IProgress
    {
        ICell<int> UpperMaxStep { get; set; }
        ICell<int> LowerMaxStep { get; set; }
        ICell<int> UpperCurrentStep { get; set; }
        ICell<int> LowerCurrentStep { get; set; }
        IStringCell Info { get; set; }
        IStringCell Debug { get; set; }
        IStringCell Exception { get; set; }
        IStringCell CurrentStatus { get; set; }
        double UpperStepPercent { get;}
    }

    public class ProgressViewModel : AbstractNestedViewModel, IProgressViewModel
    {
        public IStringCell Debug { get; set; }

        public IStringCell Exception { get; set; }

        public IStringCell Info { get; set; }

        public ICell<int> LowerCurrentStep { get; set; }

        public ICell<int> LowerMaxStep { get; set; }

        public ICell<int> UpperCurrentStep { get; set; }

        public ICell<int> UpperMaxStep { get; set; }

        public IStringCell CurrentStatus { get; set; }

        public double UpperStepPercent
        {
            get
            {
                return (double)UpperCurrentStep.Value / (double)UpperMaxStep.Value;
            }
        }

        public ProgressViewModel(IViewModel parent, string PropertyName) : base(parent, PropertyName)
        {
            LoadViewModel();
        }

        public override void LoadViewModel()
        {
            CurrentStatus = new StringCell("Preparing migration...", this, nameof(CurrentStatus));
            Info = new StringCell("", this, nameof(Info));
            Debug = new StringCell("", this, nameof(Debug));
            Exception = new StringCell("", this, nameof(Exception));
            LowerCurrentStep = new Cell<int>(this, nameof(LowerCurrentStep));
            UpperCurrentStep = new Cell<int>(this, nameof(UpperCurrentStep));
            UpperCurrentStep.PropertyChanged += (s, e) => InvokePropertyChanges(nameof(UpperStepPercent));

            UpperMaxStep = new Cell<int>(this, nameof(UpperMaxStep));
            LowerMaxStep = new Cell<int>(this, nameof(LowerMaxStep));
            base.LoadViewModel();
        }
        public void OnProgress(int currentstep, int maxstep, string description)
        {
            LowerCurrentStep.Value = currentstep;
            LowerMaxStep.Value = maxstep;
            Info.AppendLine($"[{DateTime.Now}]>> {description}");
        }

        public void WriteDebug(string message)
        {
            Debug.AppendLine($"[{DateTime.Now}]>> {message}");
        }

        public void WriteException(string message, Exception ex)
        {
            Debug.AppendLine($"[ERR @ {DateTime.Now}]>> {message}Exception:\n{ex.ToString()}");
        }

    }
}
