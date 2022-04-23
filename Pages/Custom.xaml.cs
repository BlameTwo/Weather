using MyNewApp.Common;
using Newtonsoft.Json;
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
using WPFUI.Appearance;
using WpfWidgetDesktop.Utils;

namespace MyNewApp.Pages
{
    /// <summary>
    /// Custom.xaml 的交互逻辑
    /// </summary>
    public partial class Custom : Page
    {
        public readonly string guid = "core.custom";
        
        public class CFG
        {
            public bool RoundedWindow { get; set; } = false;
            public ThemeType ThemeType { get; set; } = ThemeType.Light;
            public int ThemeIndex { get; set; } = 0;

            public bool TransparentWindow { get; set; } = false;
        }

        private CustomVM vm;
        private CFG cfg;

        public Custom()
        {
            InitializeComponent();

            vm = new CustomVM();

            this.DataContext = vm;

            cfg = JsonConvert.DeserializeObject<CFG>(SettingProvider.Get(guid));
            if (cfg == null)
            {
                cfg = new CFG();
            }
            vm.RoundedWindow = cfg.RoundedWindow;
            vm.TransparentWindow = cfg.TransparentWindow;
            vm.ThemeIndex = cfg.ThemeIndex;
        }

        private void DarkMode(object sender, RoutedEventArgs e)
        {
            WPFUI.Appearance.Theme.Set(
                 ThemeType.Dark ,
                BackgroundType.Mica, true, false);
            cfg.ThemeType = ThemeType.Dark;
        }

        private void LightMode(object sender, RoutedEventArgs e)
        {
            WPFUI.Appearance.Theme.Set(
                 ThemeType.Light,
                BackgroundType.Mica, true, false);
            cfg.ThemeType = ThemeType.Light;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //vm=new CustomVM();

            //this.DataContext = vm;

            //cfg = JsonConvert.DeserializeObject<CFG>(SettingProvider.Get(guid));
            //if(cfg == null)
            //{
            //    cfg=new CFG();
            //}
            //vm.RoundedWindow = cfg.RoundedWindow;
            //vm.ThemeIndex = cfg.ThemeIndex;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            cfg.RoundedWindow = vm.RoundedWindow;
            cfg.ThemeIndex = vm.ThemeIndex;
            cfg.TransparentWindow = vm.TransparentWindow;   
            SettingProvider.SetNoSave(guid,cfg);
        }
    }
    public class CustomVM : NotifyBase
    {
        private bool _rounded;

        public bool RoundedWindow
        {
            get { return _rounded; }
            set { _rounded = value; DoNotify(); }
        }

        private int _themeIndex;

        public int ThemeIndex
        {
            get { return _themeIndex; }
            set { _themeIndex = value; DoNotify(); }
        }

        private bool _transparent;

        public bool TransparentWindow
        {
            get { return _transparent; }
            set { _transparent = value; DoNotify(); }
        }

    }
}
