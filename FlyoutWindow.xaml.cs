using MyNewApp.Common;
using MyNewApp.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Utils;
using WPFUI.Appearance;
using WpfWidgetDesktop.Utils;

namespace MyNewApp
{
    /// <summary>
    /// FlyoutWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FlyoutWindow : Window
    {
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        public readonly string guid = "core.flyoutwindow";

        public class CFG
        {
            public string guid;
            public double left;
            public double top;
            public bool TopMost;
        }

        private CFG cfg;

        public static Dictionary<string, CFG> CFGS;

        private IWidgetBase widget;

        public FlyoutWindow(UserControl widget)
        {
            InitializeComponent();

            this.widget = (IWidgetBase?)widget;

            IntPtr hWnd = new WindowInteropHelper(GetWindow(this)).EnsureHandle();


            ResizeMode = this.widget.WCanResize? ResizeMode.CanResizeWithGrip:ResizeMode.NoResize;
            

            Height =this. widget.WidgetHeight + 2;
            Width =this. widget.WidgetWidth + 2;
            this.widget.action = CloseWidget;

            


            var othercfg = JsonConvert.DeserializeObject<Custom.CFG>(SettingProvider.Get("core.custom"));
            if(othercfg != null)
            {
                if (othercfg.RoundedWindow)
                {
                    bd.CornerRadius = new CornerRadius(10);
                }
                else
                {
                    bd.CornerRadius = new CornerRadius(0);
                }

                if (othercfg.TransparentWindow)
                {
                    bd.Background = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0));
                }
                WPFUI.Appearance.Theme.Set(
                othercfg.ThemeType,
                BackgroundType.Mica, true, false);
            }



            this.LocationChanged += LocationChangedHandler;

            this.Loaded += LoadWidget;

            this.Closed += Window_Closed;

            SetWindowLong(hWnd, (-20), 0x80);

        }
        private void Window_Closed(object sender, EventArgs e)
        {
            WindowPos.LX.Remove(this);
            var a = (this.widget as UserControl).Parent as Grid;
            a.Children.Clear();
        }

        private void LocationChangedHandler(object s, EventArgs e)
        {
            WindowPos.GetNearBy(this);
        }

        private void LoadWidget(object sender, EventArgs e)
        {
            WindowPos.LX.Add(this);
            try
            {
                this.cfg = new CFG
                {
                    guid = widget.GUID,
                };
                CFGS = JsonConvert.DeserializeObject<Dictionary<string, CFG>>(SettingProvider.Get(guid));
                if (CFGS == null)
                {
                    CFGS = new Dictionary<string, CFG>();
                }
                if (CFGS.ContainsKey(cfg.guid))
                {

                    cfg = CFGS[cfg.guid];
                    this.Left = cfg.left;
                    this.Top = cfg.top;
                    this.Topmost = cfg.TopMost;
                }

                Container.Children.Add(widget as UserControl);
            }
            catch (Exception ex)
            {
                this.Close();
            }
        }

        public void CloseWidget()
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            CloseWidget();
            MenuItem_Click_1(null, null);
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            cfg.left = this.Left;
            cfg.top = this.Top;
            CFGS[cfg.guid] = cfg;
            SettingProvider.SetNoSave(this.guid, CFGS);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

                if (WidgetsList.MainWindow.WindowState == WindowState.Minimized)
                {
                    WidgetsList.MainWindow.WindowState = WindowState.Normal;
                }
                WidgetsList.MainWindow.Visibility = Visibility.Visible;
                WidgetsList.MainWindow.Topmost = true;
                WidgetsList.MainWindow.Topmost = false;
            }
            catch (Exception ex)
            {
                new MainWindow().Show();
            }

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();

        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Topmost = !Topmost;
            cfg.TopMost = Topmost;
        }
    }
}
