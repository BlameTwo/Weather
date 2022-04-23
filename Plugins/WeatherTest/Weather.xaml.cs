
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

namespace WeatherTest
{
    /// <summary>
    /// Weather.xaml 的交互逻辑
    /// </summary>
    public partial class Weather : UserControl, IWidgetBase
    {
        public Weather()
        {
            InitializeComponent();
            this.Loaded += Weather_Loaded;
        }

        private async void Weather_Loaded(object sender, RoutedEventArgs e)
        {
            WeatherVM vm = new WeatherVM();
            await vm.Init();
            this.DataContext = vm ;
        }

        public int WidgetHeight { get { return 200; } set { WidgetHeight = value; } }
        public int WidgetWidth { get { return 500; } set { WidgetHeight = value; } }
        public string WidgetName { get { return "每日天气"; } set { WidgetName = value; } }
        public string Description { get { return "每天好心情！"; } set { Description = value; } }
        public string Icon { get { return "CompassNorthwest20"; } set { Icon = value; } }
        public string GUID { get { return "base.Weather"; } set { GUID = value; } }
        public bool WCanResize { get; set; } = false;
        public Action action { get; set; } = null;


        public class WeatherVM : NotifyBase
        {
            public WeatherVM()
            {
                Init();
            }

            public async Task Init()
            {
                _MyWeatcher = await WeatherMain.GetWeather("110101");
            }

            private WeatherModel MyWeatcher;

            public WeatherModel _MyWeatcher
            {
                get { return MyWeatcher; }
                set { MyWeatcher = value; DoNotify(); }
            }

        }
    }
}
