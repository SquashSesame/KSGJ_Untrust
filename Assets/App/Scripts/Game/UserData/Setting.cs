using UnityEngine;
using UnityEngine.Serialization;

namespace App
{
    /// <summary>
    /// 設定・データ
    /// </summary>
    [System.Serializable]
    public class Setting
    {
        public float bgmVolAudio;
        public float seVolAudio;
        // public float
        
        public Setting()
        {
            Reset();
        }

        public void Reset()
        {
            /*
             * デフォルト値
             */
            bgmVolAudio = 1.8f;
            seVolAudio = 1.0f;
        }
        
        public void CopyFrom(Setting other)
        {
            bgmVolAudio = other.bgmVolAudio;
            seVolAudio = other.seVolAudio;
        }
    }
}