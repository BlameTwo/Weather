using MyNewApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyNewApp.Pages
{
    /// <summary>
    /// WManage.xaml 的交互逻辑
    /// </summary>
    public partial class WManage : Page
    {
        public WManage()
        {
            InitializeComponent();
        }
    }

    public static class WidgetsList
    {
        public static MainWindow MainWindow { get; set; }


        public class WidgetInstance : NotifyBase
        {
            private UserControl _widget;


            public UserControl widget
            {
                get { return _widget; }
                set { _widget = value; this.DoNotify(); }
            }


            //public WidgetBase widget { get; set; }


            //private bool enabled;

            private bool _enabled;

            public bool Enabled
            {
                get { return _enabled; }
                set
                {

                    _enabled = value;
                    if (value)
                    {
                        MainWindow.RefreshWidgets();
                    }
                    else
                    {
                        IWidgetBase iw = (IWidgetBase)widget;

                        if (iw.action != null)
                        {
                            MainWindow.SaveWidgetStatue();
                            iw.action.Invoke();
                        }
                    }
                    this.DoNotify();
                }
            }

            public Point point { get; set; }


        }

        //public static List<WidgetInstance> widgetInstances { get; set; } = new List<WidgetInstance>();
        private static List<WidgetInstance> _widgetInstances;

        public static List<WidgetInstance> widgetInstances
        {
            get { return _widgetInstances; }
            set { _widgetInstances = value; }
        }



    }

}
