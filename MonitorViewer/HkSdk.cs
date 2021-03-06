﻿using System;
using System.Runtime.InteropServices;

namespace MonitorViewer
{
    public class HkSdk
    {

        [DllImport(@"OpenNetStream.dll")]//SDK启动
        public static extern int OpenSDK_InitLib(string authAddr, string platform, string appId);


        [DllImport(@"OpenNetStream.dll")]//SDK关闭
        public static extern int OpenSDK_FiniLib();

        [DllImport(@"OpenNetStream.dll")]//SDK第三方登陆
        public static extern int OpenSDK_Mid_Login(ref string pToken, ref int tokenLth);

        [DllImport(@"OpenNetStream.dll")]
        public static extern int OpenSDK_HttpSendWithWait(string szUri, string szHeaderParam, string szBody, out IntPtr iMessage, out int iLength);

        [DllImport(@"OpenNetStream.dll")]//SDK申请会话
        public static extern int OpenSDK_AllocSession(MsgHandler callBack, IntPtr userId, ref IntPtr pSid, ref int sidLth, bool bSync, uint timeout);

        [DllImport(@"OpenNetStream.dll")]//SDK申请会话（异步）
        public static extern int OpenSDK_AllocSessionEx(MsgHandler callBack, IntPtr userId, ref IntPtr pSid, ref int sidLth);

        //及其回调函数格式
        public delegate int MsgHandler(IntPtr sid, uint msgType, uint error, string info, IntPtr pUser);

        //回调实例
        public static int HandlerWork(IntPtr sid, uint msgType, uint error, string info, IntPtr pUser) => 0;

        [DllImport(@"OpenNetStream.dll")]//SDK关闭会话
        public static extern int OpenSDK_FreeSession(string sid);

        [DllImport(@"OpenNetStream.dll")]//SDK开始播放
        public static extern int OpenSDK_StartRealPlay(IntPtr sid, IntPtr playWnd, string cameraId, string token, int videoLevel, string safeKey, string appKey, uint pNscbMsg);

        [DllImport(@"OpenNetStream.dll")]//SDK关闭播放
        public static extern int OpenSDK_StopRealPlay(IntPtr sid, uint pNscbMsg);

        [DllImport(@"OpenNetStream.dll")]//截屏
        public static extern int OpenSDK_CapturePicture(IntPtr sid, string szFileName);

        [DllImport(@"OpenNetStream.dll")]//设置数据回调窗口
        public static extern int OpenSDK_SetDataCallBack(IntPtr sid, OpenSdkDataCallBack pDataCallBack, string pUser);

        //SDK获取所有设备摄像机列表
        [DllImport(@"OpenNetStream.dll")]
        public static extern int OpenSDK_Data_GetDevList(string accessToken, int iPageStart, int iPageSize, out IntPtr iMessage, out int iLength);

        /// <summary>
        /// 云台控制
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="accessToken"></param>
        /// <param name="cameraId"></param>
        /// <param name="command"></param>
        /// <param name="action"></param>
        /// <param name="speed"></param>
        /// <param name="pNscbMsg"></param>
        /// <returns></returns>
        [DllImport(@"OpenNetStream.dll")]
        public static extern int OpenSDK_PTZCtrl(IntPtr sid, string accessToken, string cameraId, PTZCommand command,
            PTZACtion action, int speed, uint pNscbMsg);

        /// <summary>
        /// 搜索录像
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="cameraId"></param>
        /// <param name="channelNumber"></param>
        /// <param name="startTime"></param>
        /// <param name="stopTime"></param>
        /// <returns></returns>
        [DllImport(@"OpenNetStream.dll")]
        public static extern int OpenSDK_StartSearchEx(IntPtr sid, string cameraId, int channelNumber, string startTime,
            string stopTime);

        [DllImport(@"OpenNetStream.dll")]
        public static extern int OpenSDK_StartSearch(IntPtr sid, string cameraId, string token, string startTime,
            string stopTime, uint pNscbMsg);

        /// <summary>
        /// 录像回放
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="playWnd"></param>
        /// <param name="cameraId"></param>
        /// <param name="channelNumber"></param>
        /// <param name="safaKey"></param>
        /// <param name="startTime"></param>
        /// <param name="stopTime"></param>
        /// <returns></returns>
        [DllImport(@"OpenNetStream.dll")]
        public static extern int OpenSDK_StartPlayBackEx(IntPtr sid, IntPtr playWnd, string cameraId, int channelNumber, string safaKey,
            string startTime, string stopTime);


        [DllImport(@"OpenNetStream.dll")]
        public static extern int OpenSDK_StartPlayBack(IntPtr sid, IntPtr playWnd, string cameraId, string token, string safeKey, string startTime,
            string stopTime, string appKey, uint pNscbMsg);

        [DllImport(@"OpenNetStream.dll")]
        public static extern int OpenSDK_StopPlayBack(IntPtr sid, uint pNscbMsg);

        //回调函数格式
        public delegate void OpenSdkDataCallBack(CallBackDateType dateType, IntPtr dateContent, int dataLen, string pUser);

        public static void DataCallBackHandler(CallBackDateType dataType, IntPtr dataContent, int dataLen, string pUser)
        {

        }

        [DllImport(@"OpenNetStream.dll")]
        public static extern string OpenSDK_GetLastErrorDesc();

        [DllImport(@"OpenNetStream.dll")]
        public static extern int OpenSDK_GetLastErrorCode();

        //数据回调设置
        public enum CallBackDateType
        {
            NetDvrSyshead = 0,
            NetDvrStreamdata = 1,
            NetDvrRecvEnd = 2,
        };
    }
}
