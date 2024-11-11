using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace App
{
    public class ScenarioForGame : MonoBehaviour
    {
        /// <summary>
        /// プロローグ
        /// </summary>
        public void Event_Prologue()
        {
            StartCoroutine(Scenario_Prologue());
        }

        [SerializeField]
        private MessageControll Window {
            get => Main.Instance.ScenarioMessage;
        }

        IEnumerator Scenario_Prologue()
        {
            Fader.SetFadeColor(Color.black);
            Fader.SetFadeLevel(1.0f);
            Window.ClearMessage();
            Window.ForceClose();

            Window.SetActiveSmartphoneButton(false);
            
            yield return Window.OpenTelop("これは真実かどうかを見極めるゲームである", 0);
            yield return Fader.YieldFadeIn(ConstDef.FADETIME);
            yield return Window.WaitTouch();
            yield return Window.CloseTelop();

            yield return Window.OpenMessage();
            yield return Window.Message("Hi, how are you?");
            yield return Window.Message("I need to tell you something");
            yield return Window.CloseMessage();

            Window.SetActiveSmartphoneButton(true);

            yield return Window.OpenMessage();
            yield return Window.Message("Well, have you heard that the ducks are leaving lake Biwa?");
            yield return Window.Message("The toxins in the lake are increasing and the fish are becoming contaminated.");
            yield return Window.Message("So the ducks started dying out. Many have flocked to other places.");
            yield return Window.Message("Many of them won’t be returning, or so it seems.");
            yield return Window.Select("Believe", "Don't Believe");
            switch (Window.SelectResult()) {
                case 0:
                    yield return Window.Message("I worry for the future of our ecosystem.");
                    break;
            
                case 1:
                    yield return Window.Message("Really?...");
                    break;

            }

            yield return Window.CloseMessage();

            yield return Fader.YieldFadeOut(ConstDef.FADETIME);
            
        }
    }
}