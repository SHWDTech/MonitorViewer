using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MonitorViewer
{
    public static class HikCameraControl
    {
        private const string PostUrl = "https://open.ys7.com/api/method";

        private const string Key = "9f88209c239d4bf28156d3f880bb8321";

        private const string Secret = "f013a79dd3c9966123fd408be34c557e";

        private const string SdkVersion = "1.0";

        private const string PhoneNumber = "18701987043";

        private const int Id = 65535;

        private static string _accessToken = string.Empty;

        public static string DeviceSerial = string.Empty;

        public static int Speed = 2;

        public static int ChannelNumber = 1;

        public static string ControlPlatform(string dir)
        {
            StopControlPlatform();

            if (!GetAccessToken())
                return "-1";

            var post = $"accessToken={_accessToken}&deviceSerial={DeviceSerial}&channelNo={ChannelNumber}&direction={GetDir(dir)}&speed={Speed}";

            var receiveString = PostWebRequest("https://open.ys7.com/api/lapp/device/ptz/start", post);

            var result = (JObject) JsonConvert.DeserializeObject(receiveString);

            return result["code"].ToString();
        }

        public static string StopControlPlatform()
        {
            if (!GetAccessToken())
                return "-1";

            var post = $"accessToken={_accessToken}&deviceSerial={DeviceSerial}&channelNo={1}";

            var receiveString = PostWebRequest("https://open.ys7.com/api/lapp/device/ptz/stop", post);

            var result = (JObject)JsonConvert.DeserializeObject(receiveString);

            return result["code"].ToString();
        }


        /// <summary>
        /// Gets the access token.
        /// </summary>
        private static bool GetAccessToken()
        {
            var utc = GetUnixTimeStamp();

            var sign = GetSign($"phone:{PhoneNumber},method:{HikMethod.GetAccessToken},time:{utc},secret:{Secret}");

            var system = new Dictionary<string, object>() { {"sign", sign}, {"time", utc}, {"ver", SdkVersion}, {"key", Key} };

            var par = new Dictionary<string, object>() { {"phone", PhoneNumber} };

            var post = new Dictionary<string, object>() { {"id", Id}, {"system", system}, {"method", HikMethod.GetAccessToken}, {"params", par} };

            var receiveString = PostWebRequest(PostUrl, JsonConvert.SerializeObject(post));

            var result = (JObject)JsonConvert.DeserializeObject(receiveString);

            if (result["result"]["code"].ToString() != "200") return false;

            _accessToken = result["result"]["data"]["accessToken"].ToString();

            return true;
        }

        /// <summary>
        /// Gets the sign.
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        private static string GetSign(string param)
            => GetMd5(param);

        private static string PostWebRequest(string postUrl, string postString)
        {
            var data = Encoding.UTF8.GetBytes(postString);
            var request = (HttpWebRequest)WebRequest.Create(postUrl);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            request.ContentLength = data.Length;
            var reqstream = request.GetRequestStream();
            reqstream.Write(data, 0, data.Length);
            request.Timeout = 90000;
            request.Headers.Set("Pragma", "no-cache");

            var response = (HttpWebResponse)request.GetResponse();

            var responseStream = response.GetResponseStream();

            if (responseStream == null) return string.Empty;

            string ret;
            using (var responseReader = new StreamReader(responseStream, Encoding.UTF8))
            {
                ret = responseReader.ReadToEnd();
            }
            response.Close();
            return ret;
        }

        private static int GetDir(string dir)
        {
            switch (dir)
            {
                case "Up":
                    return 0;
                case "Down":
                    return 1;
                case "Left":
                    return 2;
                case "Right":
                    return 3;
            }

            return 0;
        }

        private static string GetUnixTimeStamp()
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return $"{Convert.ToInt64((DateTime.Now.AddHours(-8) - start).TotalMilliseconds) / 1000}";
        }

        private static string GetMd5(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return string.Empty;
            }

            var md5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(str))).ToLower().Replace("-", "");
        }
    }
}