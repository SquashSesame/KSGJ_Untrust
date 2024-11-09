using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        }

        public void StartGame()
        {
        }
        
        public void EndGame()
        {
        }

        void Start ()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}