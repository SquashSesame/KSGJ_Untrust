using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App
{

    public class StaffrollMain : Singleton<StaffrollMain>
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Fader.FadeIn();
        }

        public void EndOfGame()
        {
            StartCoroutine(Proc_EndOfGame());
        }

        IEnumerator Proc_EndOfGame()
        {
            yield return Fader.YieldFadeOut();

            SceneManager.LoadScene("Title");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}