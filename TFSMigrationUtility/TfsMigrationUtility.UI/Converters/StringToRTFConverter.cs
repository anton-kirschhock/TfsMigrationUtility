using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace TfsMigrationUtility.UI.Converters
{

    public class StringToRTFConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FlowDocument doc = new FlowDocument();
            if(value is string)
            {
                using (StringReader sr = new StringReader(value as string))
                {
                    var ln = "";
                    while((ln = sr.ReadLine()) != null)
                    {
                        Paragraph p =  new Paragraph(new Run(ln));
                        if (ln.Contains("WARNING:"))
                        {
                            p.Foreground = new SolidColorBrush(Colors.Red);
                            p.FontWeight = FontWeights.Bold;
                        }else if (ln.Contains("SUCCESS:"))
                        {
                            p.Foreground = new SolidColorBrush(Colors.DarkGreen);
                            p.FontWeight = FontWeights.Bold;
                        }
                        doc.Blocks.Add(p);
                    }
                }
            }
            return doc;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is FlowDocument)
            {
                FlowDocument doc = value as FlowDocument;
                return new TextRange(doc.ContentStart, doc.ContentEnd).Text;
            }
            return null;
        }
    }
}
