
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTest
{
    public class WeatherMain
    {
        public const string KEY = "cc4afcc3bbae537ff7ef54fe2024e560";


        /// <summary>
        /// 天气解析
        /// </summary>
        /// <param name="citycode">城市代码</param>
        /// <param name="extensions">实况天气</param>
        /// <param name="Output">返回格式</param>
        /// <returns></returns>
        static async Task<string> GetWeatherJson(string citycode,string extensions="all",string Output="JSON")
        {
            return await Task.Run(async () =>
            {
                string url = @$"https://restapi.amap.com/v3/weather/weatherInfo?key={KEY}&city={citycode}";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                //返回json字符串
                return myStreamReader.ReadToEnd();
            });
        }

        internal static Task GetWeather()
        {
            throw new NotImplementedException();
        }

        public static async Task<WeatherModel> GetWeather(string citycode, string extensions = "all", string Output = "JSON")
        {
            return await Task.Run(async () =>
            {
                JObject jobject = JObject.Parse(await GetWeatherJson(citycode, extensions, Output));
                if (jobject["status"].ToString() == "0")            //返回代码为错误
                    return new WeatherModel();          //返回一个新的值，但是是空的
                WeatherModel wm = new WeatherModel()
                {
                    State = jobject["status"].ToString(),
                    Count = jobject["count"].ToString(),    
                    info = jobject["info"].ToString(),
                    infostring = jobject["infocode"].ToString()
                };
                wm.live = new ObservableCollection<Live>();
                var array = jobject["lives"].ToString();
                JArray ja = JArray.Parse(array);
                foreach (var item in ja)
                {
                    Live live = new Live();
                    live.city = item["province"].ToString();
                    live.LittleCity = item["city"].ToString();
                    live.Code = item["adcode"].ToString();
                    live.WeatherString = item["weather"].ToString();
                    live.Temp = item["temperature"].ToString();
                    live.ForFlage = item["winddirection"].ToString();
                    live.PowerFlage = item["windpower"].ToString();
                    live.humidity = item["humidity"].ToString();
                    live.Time = item["reporttime"].ToString();
                    wm.live.Add(live);
                }
                return wm;
            });
        }


    }


    public class WeatherModel
    {
        public string State { get; set; }
        public string Count { get; set; }
        public string info { get; set; }    
        public string infostring { get; set; }
        public ObservableCollection<Live> live { get; set; }
    }

    public class Live
    {
        public string city { get; set; }
        public string LittleCity { get; set; }
        public string Code { get; set; }
        public string WeatherString { get; set; }
        public string Temp { get; set; }
        public string ForFlage { get; set; }
        public string PowerFlage { get; set; }
        public string humidity { get; set; }
        public string Time { get; set; }
    }
}
