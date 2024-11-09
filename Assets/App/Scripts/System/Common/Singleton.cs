using UnityEngine;

namespace App
{
    /// <summary>
    /// シングルトン（そのシーン終了と同時に破棄される）
    /// </summary>
    public abstract class Singleton<T>
        : MonoBehaviour where T : Singleton<T>
    {
        private static T s_instance = null;

        public static T Instance {
            get {
                if (s_instance == null) {
                    // ここで初めて生成されたとき
                    s_instance = (T) FindObjectOfType(typeof(T));
                    if (s_instance == null) {
                        UnityEngine.Debug.LogWarning(typeof(T) + "is nothing");
                    }
                }

                return s_instance;
            }
        }

        protected virtual void Awake()
        {
            if (s_instance == null) {
                // 初めて生成されたとき
                s_instance = (T) this;
            }
            else if (s_instance != this) {
                // 既に同じコンポーネントが存在する場合
                Destroy(this.gameObject);
                return;
            }
        }
    }


    /// <summary>
    /// シングルトン（そのシーン終了しても存続）
    /// </summary>
    public abstract class SingletonDontDestroy<T>
        : MonoBehaviour where T : SingletonDontDestroy<T>
    {
        private static T s_instance = null;

        public static T Instance {
            get {
                if (s_instance == null) {
                    // ここで初めて生成されたとき
                    s_instance = (T) FindObjectOfType(typeof(T));
                    if (s_instance == null) {
                        UnityEngine.Debug.LogWarning(typeof(T) + "is nothing");
                    }
                }

                return s_instance;
            }
        }

        protected virtual void Awake()
        {
            if (s_instance == null) {
                // 初めて生成されたとき
                s_instance = (T) this;
                // シーンを跨げるオブジェクトへ
                DontDestroyOnLoad(this.gameObject);
            }
            else if (s_instance != this) {
                // 既に同じコンポーネントが存在する場合
                Destroy(this.gameObject);
                return;
            }
        }
    }
}