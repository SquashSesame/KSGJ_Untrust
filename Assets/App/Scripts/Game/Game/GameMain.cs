using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace App
{
    public enum GamePhase                                                                                            
    {
        NONE,
        TITLE,
        GAME,
        ENDING,
        //
        MAX,
    }

    public class GameMain : SingletonDontDestroy<GameMain>
    {
        public GamePhase gamePhase;

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
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}