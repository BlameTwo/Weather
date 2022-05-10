using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyNewApp.Common
{
    public interface IWidgetBase
    {

        int WidgetHeight { get; set; }
        int WidgetWidth { get; set; }

        string WidgetName { get; set; }
        string Description { get; set; }


        string Icon { get; set; }

        string GUID { get; set; }

        bool WCanResize { get; set; }


        Action action { get; set; }
    }
}
