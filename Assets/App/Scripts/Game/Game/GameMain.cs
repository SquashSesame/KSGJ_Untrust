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
        public void InitializeGame()
        {
            // Fader
            Fader.SetFadeColor(Color.black);
            Fader.SetFadeLevel(1.0f);
            Fader.FadeIn(ConstDef.FADETIME);
        }

        public void StartGame()
        {
            Fader.FadeOut(ConstDef.FADETIME, Fader.Layer.MAIN, () => {
                SceneManager.LoadScene("Main");
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
                ret = Input.GetMouseButton(0) || Input.touchCount > 0;
                hasPressed = ret;
            }
            else {
                if (Input.GetMouseButton(0) == false && Input.touchCount <= 0) {
                    hasPressed = false;
                }
            }

            return ret;
        }
    }
}