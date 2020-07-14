using UnityEngine;
using VRPanorama;

namespace VRPanorama
{
    static class FFmpegConfig
    {
        public static string BinaryPath
        {
            get {
                var basePath = Application.dataPath + "\\VRPanorama\\StreamingAssets\\";
                
                if (Application.platform == RuntimePlatform.OSXPlayer ||
                    Application.platform == RuntimePlatform.OSXEditor)
                    return basePath + "ffmpeg";

                if (Application.platform == RuntimePlatform.LinuxPlayer ||
                    Application.platform == RuntimePlatform.LinuxEditor)
                    return basePath + "ffmpeg";
                Debug.Log(basePath + "ffmpeg.exe");
                return basePath + "ffmpeg.exe";
            }
        }

        public static bool CheckAvailable
        {
            get { return System.IO.File.Exists(BinaryPath); }
        }
    }
}
