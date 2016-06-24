using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace MonitorViewer
{
    public static class ServerConnecter
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        public static string Server { get; set; }

        /// <summary>
        /// 获取摄像头连接初始化参数
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetCameraInfomation()
        {
            var requestParams = new Dictionary<string, object> {{"requestUrl", "GetCameraServerInfo"}};
            var jsonParams = ServerRequest("GET", requestParams);

            if (jsonParams == null) return null;

            var paramDictionery = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonParams);

            return paramDictionery;
        }

        /// <summary>
        /// 获取SafeKey
        /// </summary>
        /// <param name="cameraId"></param>
        /// <returns></returns>
        public static string GetSafeKey(string cameraId)
        {
            var requestParams = new Dictionary<string, object> { { "requestUrl", $"GetCameraSafeKey?id={cameraId}" } };
            var jsonParams = ServerRequest("GET", requestParams);

            return jsonParams;
        }

        public static bool PostCapturePicture(byte[] picBytes)
        {
            var base64 = System.Convert.ToBase64String(picBytes);

            var postJson = $"userName={MonitorViewer.CameraProductId}&base64Pic={base64}";

            var requestParams = new Dictionary<string, object>() { {"requestUrl", $"UploadPicture"}, {"Content", $"{postJson}" } };

            var result = ServerRequest("POST", requestParams);
            return bool.Parse(result.Replace("\\", "").Replace("\"", ""));
        }

        /// <summary>
        /// 发送服务器请求
        /// </summary>
        /// <param name="method"></param>
        /// <param name="paramDictionary"></param>
        /// <returns></returns>
        private static string ServerRequest(string method, Dictionary<string, object> paramDictionary)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://{Server}/Ajax/{paramDictionary["requestUrl"]}");
            httpWebRequest.Method = method;
            if (method == "POST")
            {
                var data = Encoding.ASCII.GetBytes(paramDictionary["Content"].ToString());
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.ContentLength = data.Length;
                var stream = httpWebRequest.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
            }

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
