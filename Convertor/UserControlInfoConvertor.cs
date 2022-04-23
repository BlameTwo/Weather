using MyNewApp.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MyNewApp.Convertor
{
    public class UserControlInfoConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var p = int.Parse( parameter as string);
            var w = (IWidgetBase)value;
            switch (p)
            {
                case 0: return w.Icon;
                case 1:return w.WidgetName;
                case 2:return w.Description;
                default : return w.Icon;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
