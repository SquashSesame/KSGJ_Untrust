using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace App
{
    public class ScenarilForGame : Singleton<ScenarilForGame>
    {
        /// <summary>
        /// プロローグ
        /// </summary>
        public void Event_Prologue()
        {
            StartCoroutine(Scenario_Prologue());
        }

        [SerializeField] private MessageControll Window;

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
            yield return Window.Message("How’s it going?");
            yield return Window.Message("We haven’t seen each other in a while.");
            yield return Window.Message("What have you been up to?");
            yield return Window.Message("I need to tell you something");
            yield return Window.CloseMessage();

            Window.SetActiveSmartphoneButton(true);

            // yield return Window.OpenMessage();
            // yield return Window.Message("Which one do you like?", false);
            // yield return Window.Select("Apple", "Google", "Amazon");
            // switch (Window.SelectResult()) {
            //     case 0:
            //         yield return Window.Message("I like an Apple!!");
            //         break;
            //
            //     case 1:
            //         yield return Window.Message("I like a Google!!");
            //         break;
            //
            //     case 2:
            //         yield return Window.Message("I like an Amazon!!");
            //         break;
            // }
            //
            // yield return Window.Message("I need a lot of money!!");
            // yield return Window.CloseMessage();

            yield return Fader.YieldFadeOut(ConstDef.FADETIME);
            
        }
    }
}