using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace App
{
    public class GameMain : SingletonDontDestroy<GameMain>
    {
        [SerializeField] private Camera uiCamera;
        
        public void InitializeGame()
        {
            // Fader
            Fader.SetFadeColor(Color.black);
            Fader.SetFadeLevel(1.0f);
            Fader.FadeIn(ConstDef.FADETIME);

            uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();
        }

        public void StartGame()
        {
            Fader.FadeOut(ConstDef.FADETIME, Fader.Layer.MAIN, () => {
                SceneManager.LoadScene("Main");
                uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();
                Fader.FadeIn(ConstDef.FADETIME);
            });
        }
        
        public void EndGame()
        {
        }

        void Start ()
        {
            InitializeGame();
            hasPressed = false;
        }

        // Update is called once per frame
        void Update()
        {
            // フレームでタッチを初期化
            if (hasPressed){
                if (Input.touchCount == 0 && Input.GetMouseButton(0) == false) {
                    hasPressed = false;
                }
            }
        }

        public void TouchRequest()
        {
            
        }

        public void DestoryTouchIcon()
        {
            
        }

        private bool hasPressed = false;
        public bool IsTouched()
        {
            bool ret = false;
            if (hasPressed == false) {
                Vector2 touchPoint = new Vector2();
                if (Input.GetMouseButton(0)) {
                    touchPoint = Input.mousePosition;
                    ret = true;
                    hasPressed = true;
                }
                else if (Input.touchCount > 0) {
                    touchPoint = Input.touches[0].position;
                    ret = true;
                    hasPressed = true;
                }
                 
                if (ret) {
                    // スマートフォンボタンのときは無視する
                    var rectTrans = smartphone.Instance.GetComponent<RectTransform>();
                    Vector2 localPoint = new Vector2();
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTrans, touchPoint, GetComponent<Camera>(), out localPoint);
                    if (rectTrans.rect.Contains(localPoint)) {
                        ret = false;
                        hasPressed = false;
                    }
                }
            }
            return ret;
        }
    }
}