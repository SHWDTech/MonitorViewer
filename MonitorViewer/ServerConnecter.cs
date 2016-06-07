using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace MonitorViewer
{
    public static class ServerConnecter
    {
        private static string _server;
        public static Dictionary<string, string> GetCameraInfomation(string server)
        {
            _server = server;
            var httpWebRequest = (HttpWebRequest) WebRequest.Create($"http://{_server}/Ajax/GetCameraServerInfo");
            httpWebRequest.Method = "GET";

            var response = httpWebRequest.GetResponse();

            var responseStream = response.GetResponseStream();

            if (responseStream != null)
            {
                using (var stream = new StreamReader(responseStream, Encoding.UTF8))
                {
                    var jsonParams = stream.ReadToEnd();
                    var paramDictionery = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonParams);
                    return paramDictionery;
                }
            }

            return null;
        }

        public static string GetSafeKey(string cameraId)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://{_server}/Ajax/GetCameraSafeKey?id={cameraId}");
            httpWebRequest.Method = "GET";

            var response = httpWebRequest.GetResponse();

            var responseStream = response.GetResponseStream();

            if (responseStream != null)
            {
                using (var stream = new StreamReader(responseStream, Encoding.UTF8))
                {
                    var result = stream.ReadToEnd();
                    return result;
                }
            }

            return null;
        }
    }
}
