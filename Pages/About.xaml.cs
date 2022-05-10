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

namespace MyNewApp.Pages
{
    /// <summary>
    /// About.xaml 的交互逻辑
    /// </summary>
    public partial class About : Page
    {
        public About()
        {
            InitializeComponent();
            ver.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
        private void Hyperlink_Click_1(object sender, RoutedEventArgs e)
        {
            BasicMethods.runcmd("start https://space.bilibili.com/298733903");

        }

        private void Hyperlink_Click_2(object sender, RoutedEventArgs e)
        {
            BasicMethods.runcmd("start https://www.bilibili.com/read/cv14950240");

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BasicMethods.runcmd("start https://gitee.com/swetycore/fluent-widgets");

        }
    }
}
