using MyNewApp.Common;
using MyNewApp.Pages;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using WpfWidgetDesktop.Utils;
using static MyNewApp.Common.PluginBase;
using static MyNewApp.Common.PluginBase.PluglingLoader;

namespace MyNewApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly string guid = "core.main";

        public static bool isRunning=false;

        public Dictionary<string, bool> cfg = new Dictionary<string, bool>();

        public void SaveWidgetStatue()
        {
            foreach (var a in WidgetsList.widgetInstances)
            {
                IWidgetBase w = (IWidgetBase)a.widget;
                cfg[w.GUID] = a.Enabled;

            }
            SettingProvider.SetNoSave(guid, cfg);
        }

        private UserControl CreateInstance(Type t)
        {
            return Activator.CreateInstance(t) as UserControl;

        }

        public void RefreshWidgets()
        {

            SaveWidgetStatue();

            foreach(var a in WidgetsList.widgetInstances)
            {
                var w = (a.widget as UserControl).Parent as Grid;
                if (w != null)
                {
                    continue;
                }
                else if(a.Enabled)
                {
                    var fw = new FlyoutWindow(a.widget);
                    fw.Show();
                }

            }




        }



        private void ScanWidgets()
        {

            WidgetsList.MainWindow = this;
            WidgetsList.widgetInstances = new List<WidgetsList.WidgetInstance>();

            //Assembly currentAssembly = Assembly.GetExecutingAssembly();
            //currentAssembly.ExportedTypes.Where(t => t.BaseType == typeof(UserControl)).ToList().ForEach(t =>
            //{
            //    Console.WriteLine(t.Name);
            //    WidgetsList.widgetInstances.Add(new WidgetsList.WidgetInstance()
            //    {
            //        widget = CreateInstance(t),
            //    });
            //});

            PluglingLoader loader=new PluglingLoader();

            LoadedPlugins plugins =loader.Load();

            MyPlugin.Plugins = plugins.Plugins;

            foreach (var plugin in plugins.userControls)
            {
                WidgetsList.widgetInstances.Add(new WidgetsList.WidgetInstance()
                {
                    widget = plugin
                }) ;
            }

            var cfg = JsonConvert.DeserializeObject<Dictionary<string, bool>>(SettingProvider.Get(guid));

            foreach (var a in WidgetsList.widgetInstances)
            {
                try
                {
                    IWidgetBase w = (IWidgetBase)a.widget;

                    if (cfg.Keys.Contains(w.GUID))
                    {

                        a.Enabled = cfg[w.GUID];
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            var c = WidgetsList.widgetInstances.Where(a =>
            {

                return a.Enabled;
            }).ToList().Count;
            if (c >= 1)
            {
                this.Visibility = Visibility.Hidden;
                //this.WindowState = WindowState.Minimized;
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            if (!isRunning)
            {
                ScanWidgets();
                isRunning = true;
            }
            WPFUI.Appearance.Background.Apply(this, WPFUI.Appearance.BackgroundType.Mica);
        }
    }
}
