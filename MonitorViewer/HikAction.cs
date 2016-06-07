using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MonitorViewer
{
    public static class HikAction
    {
        private static string ApiUrl { get; set; }

        private static string AuthAddr { get; set; }

        private static string PlatformAddr { get; set; }

        private static string AppKey { get; set; }

        private static string SecretKey {get; set; }

        private static string PhoneNumber { get; set; }

        private static string AccessToken { get; set; }

        public static string SafeKey { get; set; }

        public static bool IsLoaded = false;

        private static string UserId { get; set; }

        private static readonly HkSdk.MsgHandler CallBack = HandlerWork;

        private static IntPtr _sessionId;

        private static int _sessionIdLength;

        public static string SessionIdstr { get; private set; }

        private static int _playLever = 2;

        public static readonly List<string> CameraList = new List<string>();

        public static readonly List<string> CameraIdList = new List<string>();

        public static int Speed { get; set; } = 4;

        public static int InitLib()
        {
            var result = HkSdk.OpenSDK_InitLib(AuthAddr, PlatformAddr, AppKey);
            if (result != 0) return result;

            result = AllocSession();
            if (result != 0) return result;

            result = GetAccessToken();
            if (result != 0) return result;

            result = FatchCameraList();
            if (result != 0) return result;

            return result;
        }

        public static int SetUpParams(Dictionary<string, string> paramDictionary)
        {
            ApiUrl = paramDictionary["ApiUrl"];
            AuthAddr = paramDictionary["AuthAddr"];
            PlatformAddr = paramDictionary["PlatformAddr"];
            AppKey = paramDictionary["AppKey"];
            SecretKey = paramDictionary["SecretKey"];
            PhoneNumber = paramDictionary["PhoneNumber"];
            UserId = paramDictionary["UserId"];

            return 0;
        }

        public static int Close() => CloseAllocion(_sessionId);

        private static int AllocSession()
        {
            var userId = Marshal.StringToHGlobalAnsi(UserId);

            var result = HkSdk.OpenSDK_AllocSession(CallBack, userId, ref _sessionId, ref _sessionIdLength, false, uint.MaxValue);

            SessionIdstr = Marshal.PtrToStringAnsi(_sessionId, _sessionIdLength);

            return result;
        }

        private static int CloseAllocion(IntPtr sid)
        {
            var sid1 = sid.ToString();

            return HkSdk.OpenSDK_FreeSession(sid1);
        }

        public static bool CapturePicture(string fileName)
        {
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                return HkSdk.OpenSDK_CapturePicture(_sessionId, fileName) == 0;
            }

            return false;
        }


        private static int HandlerWork(IntPtr sessionId, uint msgType, uint error, string info, IntPtr pUser)
        {
            switch (msgType)
            {
                case 20:
                    JObject obj = (JObject)JsonConvert.DeserializeObject(info);
                    Console.WriteLine(obj);
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }

            return 0;
        }

        private static int GetAccessToken()
        {
            IntPtr iMessage;
            int iLength;
            var jsonStr = BuildParams("token");

            var result = HkSdk.OpenSDK_HttpSendWithWait(ApiUrl, jsonStr, "", out iMessage, out iLength);

            var returnStr = Marshal.PtrToStringAnsi(iMessage, iLength);

            if (result == 0)
            {
                JObject jObject = (JObject)JsonConvert.DeserializeObject(returnStr);
                if (jObject["result"]["code"].ToString() == "200")
                {
                    AccessToken = jObject["result"]["data"]["accessToken"].ToString();

                    Debug.WriteLine(AccessToken);

                }
                else
                {
                    Debug.WriteLine(jObject["result"]["code"].ToString());
                }
            }

            return result;
        }

        private static string BuildParams(string type)
        {
            var str = string.Empty;
            var typestr = type.ToLower();

            if (typestr != "token")
            {
                if (typestr == "msg")
                {
                    str = "msg/get";
                }
            }
            else
            {
                str = "token/getAccessToken";
            }

            var span = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1, 0, 0, 0));
            var totalSeconds = $"{Math.Round(span.TotalSeconds, 0)}";
            var md5 = GetMd5($"phone:{PhoneNumber},method:{str},time:{totalSeconds},secret:{SecretKey}");

            return ("{\"id\":\"100\",\"system\":{\"key\":\"" + AppKey + "\",\"sign\":\"" + md5 + "\",\"time\":\"" + totalSeconds +
                "\",\"ver\":\"1.0\"},\"method\":\"" + str + "\",\"params\":{\"phone\":\"" + PhoneNumber + "\"}}");
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

        private static int FatchCameraList()
        {
            IntPtr iMessage;
            int iLength;

            int result = HkSdk.OpenSDK_Data_GetDevList(AccessToken, 0, 50, out iMessage, out iLength);
            var resultStr = Marshal.PtrToStringAnsi(iMessage);

            if (result != 0) return result;

            var jsonObj = JObject.Parse(resultStr);

            var cameraList = JArray.Parse(jsonObj["cameraList"].ToString());

            foreach (var cam in cameraList)
            {
                var cameraObj = JObject.Parse(cam.ToString());
                CameraList.Add(cameraObj["cameraName"].ToString());
                CameraIdList.Add(cameraObj["cameraId"].ToString());
            }

            return result;
        }

        public static int StartPlay(IntPtr handleIntPtr, string cameraId, string safeKey)
        => HkSdk.OpenSDK_StartRealPlay(_sessionId, handleIntPtr, cameraId, AccessToken, _playLever, safeKey, AppKey, 0);

        public static int StopPlay()
        {
            CloseAllocion(_sessionId);
            return HkSdk.OpenSDK_StopRealPlay(_sessionId, 0);
        }

        public static object[] GetCameraList()
        {
            var cameraObjects = new object[CameraList.Count];

            for (var i = 0; i < CameraList.Count; i++)
            {
                cameraObjects[i] = CameraList[i];
            }

            return cameraObjects;
        }

        /// <summary>
        /// 云台控制
        /// </summary>
        /// <param name="cameraId"></param>
        /// <param name="command"></param>
        /// <param name="action"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public static int PtzCtrl(string cameraId, PTZCommand command,
            PTZACtion action, int speed)
            => HkSdk.OpenSDK_PTZCtrl(_sessionId, AccessToken, cameraId, command, action, speed, 0);


        public static object[] GetCameraIdList()
        {
            var cameraIdObjects = new object[CameraIdList.Count];

            for (var i = 0; i < CameraIdList.Count; i++)
            {
                cameraIdObjects[i] = CameraIdList[i];
            }

            return cameraIdObjects;
        }
    }
}
