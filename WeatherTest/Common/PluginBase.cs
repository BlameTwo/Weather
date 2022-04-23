using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WeatherTest.Common
{
    public class PluginBase
    {
        public interface IPlugin
        {
            public string Name => "插件名称";
            public string Description => "插件描述";
            public string Author => "Author";
            //[Obsolete] public bool IsEnabled { get; set; }
        }

        public class PluglingLoader
        {

            public class LoadedPlugins
            {
                public  List<IPlugin> Plugins { get; set; }=new List<IPlugin>();

                public  List<UserControl> userControls { get; set; }=new List<UserControl>();


            }
            public LoadedPlugins Load()
            {
                var url = Environment.CurrentDirectory + "\\Plugins";
                if (!Directory.Exists(url))         　　              
                    Directory.CreateDirectory(url);//创建该文件夹　　


                LoadedPlugins lp = new LoadedPlugins();
                string[] files = Directory.GetFiles(url);

                //将所有plugin文件夹下面的文件路径读取到files里面

                foreach (string file in files)

                {

                    if (file.ToUpper().EndsWith(".DLL"))//将文件名转换为大写，如果后缀为".DLL"则为插件,执行下面语句

                    {

                        Assembly ab = Assembly.LoadFile(file);//可以将assembly视为一个程序集的类型，通过loadfile将其实例化

                        Type[] types = ab.GetTypes();//获取程序集中程序的类型

                        foreach (Type t in types)

                        {

                            if (t.GetInterface("IPlugin") != null)//搜索带有指定接口的插件

                            {
                                lp.Plugins.Add(ab.CreateInstance(t.FullName) as IPlugin);//将插件装入plugins集合
                                Console.WriteLine($"检测到插件:{t.FullName}");
                            }

                            if (t.GetInterface("IWidgetBase") != null)
                            {
                                lp.userControls.Add(ab.CreateInstance(t.FullName) as UserControl);
                                Console.WriteLine($"检测到小部件:{t.FullName}");

                            }

                        }

                    }

                }
                return lp;
            }

        }
    }
}
