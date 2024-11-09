using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace App
{
    public class Fader : SingletonDontDestroy<Fader>
    {
        public enum Layer
        {
            MAIN,
            SUB,
        }
        
        #region フェードレベルと色に合わせて更新

        // エディタからスライダーで0.0～1.0と制限を設ける
        [SerializeField, Range(0.0f, 1.0f)] float fadeMainLevel;
        [SerializeField, Range(0.0f, 1.0f)] float fadeSubLevel;
        // [SerializeField] private Layer _layer = Layer.MAIN;

        public float Range
        {
            get { return fadeMainLevel; }
            set
            {
                fadeMainLevel = value;
                UpdateFade(fadeMainLevel);
            }
        }

        [SerializeField] UnityEngine.UI.Image fadeMainLayer = null;
        [SerializeField] UnityEngine.UI.Image fadeSubLayer = null;

        bool is_end = false;
        Material material;

        protected override void Awake()
        {
            base.Awake();

            material = fadeMainLayer.material;
        }

        void ApplyFade(float level, Layer layer = Layer.MAIN)
        {
            if (layer == Layer.MAIN || fadeSubLayer == null) {
                fadeMainLevel = level;
                Color col = fadeMainLayer.color;
                col.a = level;
                fadeMainLayer.color = col; //<- Colorを設定すると反映
                if (material != null)
                {
                    material.SetFloat("_Range", 1.0f + fadeMainLevel);
                }
            }
            else if (layer == Layer.SUB) {
                fadeSubLevel = level;
                Color col = fadeSubLayer.color;
                col.a = level;
                fadeSubLayer.color = col; //<- Colorを設定すると反映
                if (material != null)
                {
                    material.SetFloat("_Range", 1.0f + fadeSubLevel);
                }
            }
        }
        
        // フェードレベルを更新・反映
        public void UpdateFade(float level, Layer layer = Layer.MAIN)
        {
            if (level > 0.0f)
            {
                enabled = true;
            }

            // level を0.0～1.0に丸め込む
            level = Mathf.Clamp(level, 0.0f, 1.0f);

            // Canvasに追加したImageのコンポーネントを取得
            if (fadeMainLayer == null)
            {
                fadeMainLayer = GetComponent<UnityEngine.UI.Image>();
            }

            // level に合わせてalpha値を設定する
            ApplyFade(level, layer);
            
            // levelが0.0 のときは機能を停止する
            if (level <= 0.0f)
            {
                enabled = false;
            }
        }

        // フェードレベルを設定
        void setFadeLevel(float level, Layer layer = Layer.MAIN)
        {
            UpdateFade(level, layer);
        }

        public float GetFadeLevel(Layer layer=Layer.MAIN)
        {
            if (layer == Layer.MAIN || fadeSubLayer == null) {
                return fadeMainLevel;
            }
            else {
                return fadeSubLevel;
            }
        }

        // フェードカラーを設定（ホワイト／ブラックなど）
        void setFadeColor(Color color, Layer layer=Layer.MAIN)
        {
            if (layer == Layer.MAIN || fadeSubLayer == null) {
                fadeMainLayer.color = color;
                UpdateFade(fadeMainLevel);
            }
            else if (layer == Layer.SUB) {
                fadeSubLayer.color = color;
                UpdateFade(fadeSubLevel);
            }
        }

#if UNITY_EDITOR
        // エディタからスライダーを変更したときに更新する
        void OnValidate()
        {
            UpdateFade(fadeMainLevel, Layer.MAIN);
            UpdateFade(fadeSubLevel, Layer.SUB);
        }
#endif

        #endregion

        #region 時間とともにフェードイン・アウトを行う

        public IEnumerator UpdateYieldFade(float stVal, float edVal, float fadeTime, Layer layer = Layer.MAIN)
        {
            is_end = false;
            if (fadeTime > 0.0f)
            {
                // 段階的に反映
                float time = 0.0f;
                while (time < fadeTime)
                {
                    float val = Mathf.Lerp(stVal, edVal, time / fadeTime);
                    UpdateFade(val, layer);
                    time += Time.deltaTime;
                    yield return null;
                }
            }

            UpdateFade(edVal, layer);
            is_end = true;
            yield return null;
        }

        /// <summary>
        /// Static関数：フェードイン・アウトが終了したか？
        /// </summary>
        static public bool IsEnd
        {
            get { return Instance.is_end; }
        }

        /// <summary>
        /// Static関数：フェードレベル
        /// </summary>
        static public void SetFadeLevel(float level, Layer layer = Layer.MAIN)
        {
            Instance.setFadeLevel(level, layer);
        }

        /// <summary>
        /// Static関数：フェードカラー設定
        /// </summary>
        static public void SetFadeColor(Color col, Layer layer = Layer.MAIN)
        {
            Instance.setFadeColor(col, layer);
        }

        /// <summary>
        /// Static関数：フェードイン
        /// </summary>
        static public Coroutine FadeIn(float fadeTime = 1.0f, Layer layer = Layer.MAIN, Action cbFunc = null)
        {
            float level = Instance.GetFadeLevel(layer);
            if (Mathf.Approximately(level, 0.0f) == true) {
                SetFadeLevel(level, layer);
                if (cbFunc != null) {
                    cbFunc.Invoke();
                }
                return null;
            }
            else {
                return Instance.StartCoroutine(YieldFadeIn(fadeTime, layer, cbFunc));
            }
        }

        /// <summary>
        /// Static関数：フェードアウト
        /// </summary>
        static public Coroutine FadeOut(float fadeTime = 1.0f, Layer layer = Layer.MAIN, Action cbFunc = null)
        {
            float level = Instance.GetFadeLevel(layer);
            if (Mathf.Approximately(level, 1.0f) == true) {
                SetFadeLevel(level, layer);
                if (cbFunc != null) {
                    cbFunc.Invoke();
                }
                return null;
            } else {
                return Instance.StartCoroutine(YieldFadeOut(fadeTime, layer, cbFunc));
            }
        }

        /// <summary>
        /// Static関数：フェードインを終了まで待つ
        /// </summary>
        static public IEnumerator YieldFadeIn(float fadeTime = 1.0f, Layer layer = Layer.MAIN, Action cbFunc = null)
        {
            yield return Instance.UpdateYieldFade(1.0f, 0.0f, fadeTime, layer);

            if (cbFunc != null)
            {
                // 終了後コールバックを呼び出す
                cbFunc.Invoke();
            }
        }

        /// <summary>
        /// Static関数：フェードアウトを終了まで待つ
        /// </summary>
        static public IEnumerator YieldFadeOut(float fadeTime = 1.0f, Layer layer = Layer.MAIN, Action cbFunc = null)
        {
            yield return Instance.UpdateYieldFade(0.0f, 1.0f, fadeTime, layer);

            if (cbFunc != null)
            {
                // 終了後コールバックを呼び出す
                cbFunc.Invoke();
            }
        }

        #endregion

        #region シーン切り替え

        // フェードイン・アウトを挟んで次のシーンへ
        static public IEnumerator YieldSwitchScene(string sceneName, float fadeTime = 1.0f)
        {
            yield return YieldFadeOut(fadeTime);

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

            yield return YieldFadeIn(fadeTime);
        }

        /// <summary>
        /// Static関数：フェードアウト・インを挟んでシーン切り替え
        /// </summary>
        static public Coroutine SwitchScene(string sceneName, float fadeTime = 1.0f)
        {
            return Instance.StartCoroutine(YieldSwitchScene(sceneName, fadeTime));
        }

        #endregion
    }

}