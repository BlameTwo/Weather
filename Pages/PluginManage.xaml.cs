using MyWidget2.Utils;
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
using static MyNewApp.Common.PluginBase;

namespace MyNewApp.Pages
{
    /// <summary>
    /// PluginManage.xaml 的交互逻辑
    /// </summary>
    public partial class PluginManage : Page
    {
        public PluginManage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BasicMethods.runcmd("start Plugins");
        }
    }
    public static class MyPlugin
    {
        public static List<IPlugin> Plugins;
    }
}
