using MyNewApp.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static MyNewApp.Common.PluginBase;

namespace MyNewApp.Convertor
{
    public class PluginInfoConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var p = int.Parse( parameter as string);
            var w = (IPlugin)value;
            switch (p)
            {
                case 0: return w.Name;
                case 1:return w.Author;
                case 2:return w.Description;
                default : return w.Name;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
