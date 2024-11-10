using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace App
{
    public class ScenarilForGame : Singleton<ScenarilForGame>
    {
        bool _is_end = false;
        public bool IsEnd { get => _is_end; }

        int _selected = -1;
        public int Selected { get => _selected; }
        
        /// <summary>
        /// プロローグ
        /// </summary>
        public void Event_Prologue()
        {
            _is_end = false;
            StartCoroutine(Scenario_Prologue());
        }

        private MessageControll Window {
            get => MessageControll.Instance;
        }

        IEnumerator Scenario_Prologue()
        {
            Fader.SetFadeLevel(1);
            Window.ClearMessage();
            Window.ForceClose();

            yield return Fader.YieldFadeIn(ConstDef.FADETIME);
            
            yield return Window.YieldOpenTelop("これは真実かどうかを見極めるゲームである");
            yield return Window.YieldWaitTouch();
            yield return Window.YieldCloseTelop();

            yield return Window.YieldOpen();
            Window.SetMessage("ここは多くの噂が集まるバー@");
            yield return Window.YieldWaitTouch();
            Window.SetMessage("噂話を聞いてみよう@");
            yield return Window.YieldWaitTouch();
            
            yield return Window.YieldClose();
            
            Window.ClearMessage();

            // yield return Window.Select("絶対リベンジしてやろう！", "幸運をいのってるよ、ローラ！");
            // switch (Easy.SelectResult()) {
            //     case 0:
            //         yield return Easy.Message(eHumans.Player, eEmotion.HAPPY, "絶対リベンジしてやろう！");
            //         break;
            //
            //     case 1:
            //         yield return Easy.Message(eHumans.Player, eEmotion.HAPPY, "幸運をいのってるよ、ローラ！");
            //         break;
            // }
            //
            // yield return Easy.Message(eHumans.Laura, eEmotion.HAPPY, "ええ！");
            //
            // yield return Easy.YieldClearMessage();
            //
            //
            // yield return TelopWindow.Instance.YieldOpen("そして、１年後");
            // yield return TelopWindow.Instance.YieldWaitTouch();
            // yield return TelopWindow.Instance.YieldClose();
            //
            //
            // yield return Easy.Message(eHumans.Player, eEmotion.IDLE, "あれから一年…");
            // //yield return Easy.Message(eHumans.Player, eEmotion.IDLE, "武者修行を続けたんだ");
            // yield return Easy.Message(eHumans.Player, eEmotion.HAPPY, "今ならあのダンジョンにだって負けやしない!");
            // yield return Easy.Message(eHumans.Player, eEmotion.IDLE, "まず、ローラと合流しなきゃな");
            // yield return Easy.Message(eHumans.Player, eEmotion.IDLE, "ちゃんとこの町に来ているだろうか…");


            yield return Fader.YieldFadeOut(ConstDef.FADETIME);
            
        }
    }
}