#if UNITY_IOS
using System.Runtime.InteropServices;
#endif
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace App
{
    public class ShowVersion : MonoBehaviour
    {
        private TMPro.TextMeshProUGUI textVersion = null;
        
        // Start is called before the first frame update
        void Start()
        {
            textVersion = GetComponent<TextMeshProUGUI>();
            UnityEngine.Debug.Assert(textVersion != null);

            string version = Application.version;
            string build = GetCurrentBuildNumber();
            if (string.IsNullOrEmpty(build) == false)
            {
                textVersion.text = $"Ver. {version}-{build}";
            }
            else
            {
                textVersion.text = $"Version. {version}";
            }
        }

        /// <summary>
        /// Get build number in current platform (iOS or Android)
        /// </summary>
        /// <remarks> Return "0" in other platform </remarks>
        /// <returns> Build number in current platform(iOS or Android) </returns>
        public static string GetCurrentBuildNumber()
        {
#if UNITY_IOS
            return GetIOSBuildNumber();
#elif UNITY_ANDROID
            return GetAndroidVersionCode().ToString();
#elif UNITY_WEBGL
            return "";
#else
            return "0";
#endif
        }

        /// <summary>
        /// Get build number in iOS
        /// </summary>
        /// <remarks> Return "0" in other platform </remarks>
        /// <returns> Build number in iOS </returns>
        public static string GetIOSBuildNumber()
        {
#if UNITY_EDITOR
            return PlayerSettings.iOS.buildNumber;
#elif UNITY_IOS
            return GetBundleVersion();
#elif UNITY_WEBGL
            return "";
#else
            return "0";
#endif
        }

        /// <summary>
        /// Get build number in Android
        /// </summary>
        /// <remarks> Return 0 in other platform </remarks>
        /// <returns> Build number in Android </returns>
        public static int GetAndroidVersionCode()
        {
#if UNITY_EDITOR
            return PlayerSettings.Android.bundleVersionCode;
#elif UNITY_ANDROID
            using (var packageInfo = GetPackageInfo())
            {
                return packageInfo.Get<int>("versionCode");
            }
#else
            return 0;
#endif
        }

#if UNITY_IOS
        [DllImport("__Internal")]
        static extern string GetBundleVersion();
#endif

#if UNITY_ANDROID
        private static AndroidJavaObject GetPackageInfo()
        {
            using var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            using var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            using var context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
            using var packageManager = context.Call<AndroidJavaObject>("getPackageManager");
            using var packageManagerClass = new AndroidJavaClass("android.content.pm.PackageManager");
            var packageName = context.Call<string>("getPackageName");
            var activities = packageManagerClass.GetStatic<int>("GET_ACTIVITIES");
            return packageManager.Call<AndroidJavaObject>("getPackageInfo", packageName, activities);
        }
#endif
    }
}
