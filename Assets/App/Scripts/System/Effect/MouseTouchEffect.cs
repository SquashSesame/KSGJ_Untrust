using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace App
{
    public class MouseTouchEffect : SingletonDontDestroy<MouseTouchEffect>
    {
        [SerializeField] private ParticleSystem touchEffect = null;
        [SerializeField] private Camera cameraEffect = null;

        // Start is called before the first frame update
        void Start()
        {
            FindTopCamera();
            SceneManager.activeSceneChanged += (Scene _a, Scene _b) => { FindTopCamera(); };
        }

        void FindTopCamera()
        {
            var obj = GameObject.Find("EffectCamera");
            if (obj != null) {
                cameraEffect = obj.GetComponent<Camera>();
            }
            else {
                cameraEffect = null;
            }
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 _mpos = Vector2.zero;
            if (cameraEffect != null && touchEffect && (Utility.Peripheral.GetScreenTouchPoint(ref _mpos))) {
                touchEffect.transform.position =
                    cameraEffect.ScreenToWorldPoint(_mpos) + cameraEffect.transform.forward * 10;
                touchEffect.Emit(1);
            }
        }
    }
}

