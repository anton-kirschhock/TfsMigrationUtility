using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace TfsMigrationUtility.UI.View.Controls
{
    public class BindableRichTextBox : RichTextBox
    {
        public static readonly DependencyProperty DocumentProperty = DependencyProperty.Register(
            nameof(Document),
            typeof(FlowDocument),
            typeof(BindableRichTextBox),
            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnDocumentChanged))
        );
        public static readonly DependencyProperty AutoScrollProperty = DependencyProperty.Register(
            nameof(AutoScroll),
            typeof(bool),
            typeof(BindableRichTextBox)
        );

        public bool AutoScroll
        {
            get
            {
                return (bool)this.GetValue(AutoScrollProperty);
            }
            set
            {
                this.SetValue(AutoScrollProperty, value);
            }
        }
        public new FlowDocument Document
        {
            get
            {
                return (FlowDocument)this.GetValue(DocumentProperty);
            }
            set
            {
                this.SetValue(DocumentProperty, value);
            }
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            if (AutoScroll) ScrollToEnd();
        }
        public static void OnDocumentChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            RichTextBox rtb = (RichTextBox)obj;
            rtb.Document = (FlowDocument)args.NewValue;
        }
    }
}
