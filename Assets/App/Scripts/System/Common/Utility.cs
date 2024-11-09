using UnityEngine;
using System;
using System.Globalization;

namespace App.Utility
{
    /// <summary>
    /// Sprite がタッチされたか？
    /// </summary>
    public static class SpriteTouch
    {
        /// <summary>
        /// 範囲内をタッチしたかどうか？（コールバック）
        /// </summary>
        public static void TouchCheck(
            UnityEngine.Vector3 mypos, float range, System.Action OnScratched)
        {
            if (Input.touchCount > 0) {
                // Touch
                foreach (Touch touch in Input.touches) {
                    Vector3 pos = touch.position;
                    if (IsTouched(ref mypos, range, ref pos)) {
                        if (OnScratched != null) {
                            OnScratched.Invoke();
                        }
                    }
                }
            }
            else {
                // Mouse
                if (Input.GetButton("Fire1") == true) {
                    Vector3 pos = Input.mousePosition;
                    if (IsTouched(ref mypos, range, ref pos)) {
                        if (OnScratched != null) {
                            OnScratched.Invoke();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 範囲内のポジションか？
        /// </summary>
        static bool IsTouched(
            ref UnityEngine.Vector3 mypos, float range, ref UnityEngine.Vector3 scrPos)
        {
            var wpos = Camera.main.ScreenToWorldPoint(scrPos);
            wpos.z = mypos.z = 0.0f;
            float length = Vector3.Distance(wpos, mypos);
            if (length <= range) {
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// 入力関係
    /// </summary>
    public static class Peripheral
    {
        private static bool _s_Repeat = false;

        /// <summary>
        /// 画面のタッチ位置を取得
        /// </summary>
        public static bool GetScreenTouchPoint(ref UnityEngine.Vector2 outPosition, bool is_repeat = false)
        {
            if (Input.touchCount > 0) {
                if (_s_Repeat && is_repeat == false) {
                    return false;
                }

                outPosition = Input.touches[0].position;
                _s_Repeat = true;
                return true;
            }
            else if (Input.GetButton("Fire1") == true) {
                if (_s_Repeat && is_repeat == false) {
                    return false;
                }

                outPosition = Input.mousePosition;
                _s_Repeat = true;
                return true;
            }

            _s_Repeat = false;

            return false;
        }
    }

    /// <summary>
    /// 文字列関係のヘルパー
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// 指定された文字数でのみ取得（超えた場合に規定数まで）
        /// </summary>
        public static string GetLimitedString(int limitNum, string inName)
        {
            return (inName.Length > limitNum) ? inName.Substring(0, limitNum) : inName;
        }
    }

    /// <summary>
    /// Dete ヘルパー
    /// </summary>
    public static class DateHelper
    {
        public static string GetWarekiString(int year)
        {
            string wareki = "----";

            CultureInfo culture = new CultureInfo("ja-JP", true);
            culture.DateTimeFormat.Calendar = new JapaneseCalendar();

            try {
                DateTime target = new DateTime(year, 1, 1);
                if (year < 2019) {
                    wareki = target.ToString("ggyy年", culture);
                }
                else if (year < 2100) {
                    wareki = $"令和{(year - 2018).ToString()}年";
                }
            }
            catch {
            }

            return wareki;
        }

        /// <summary>
        /// DateTimeを返す
        /// </summary>
        /// <param name="warekiName"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime GetFromWareki(string warekiName, int year)
        {
            DateTime target = DateTime.MinValue;

            CultureInfo culture = new CultureInfo("ja-JP", true);
            culture.DateTimeFormat.Calendar = new JapaneseCalendar();

            try {
                if (warekiName == "西暦") {
                    target = new DateTime(year, 1, 1);
                }
                else if (warekiName == "令和") {
                    target = new DateTime(2018 + year, 1, 1);
                }
                else {
                    string wareki = $"{warekiName}{year}年1月1日";
                    target = DateTime.ParseExact(wareki, "ggyy年m月d日", culture);
                }
            }
            catch {
            }

            return target;
        }
    }

    /// <summary>
    /// 補間 
    /// </summary>
    public static class Interpolate
    {
        public static float SlowFastSlow(float first, float aim, float rate)
        {
            float rate2 = (1.0f - Mathf.Cos(Mathf.Clamp01(rate) * Mathf.PI)) * 0.5f;
            return first * (1.0f - rate2) + aim * rate2;
        }

        public static float FastSlow(float first, float aim, float rate)
        {
            float rate2 = Mathf.Sin(Mathf.Clamp01(rate) * Mathf.PI * 0.5f);
            return first * (1.0f - rate2) + aim * rate2;
        }
    }
}