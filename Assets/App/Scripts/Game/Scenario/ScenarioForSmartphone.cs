using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace App
{
    public class ScenarioForSmartphone : MonoBehaviour
    {
        /// <summary>
        /// プロローグ
        /// </summary>
        public void Event_Prologue()
        {
            StartCoroutine(Scenario_Prologue());
        }

        [SerializeField]
        private MessageControll Window
        {
            get => Main.Instance.SmartphoneMessage;
        }

        IEnumerator Scenario_Prologue()
        {
            yield return Window.CloseMessage();

            yield return Window.Select("DuckBill News: Ducks Flap from Lake Biwa", "DuckPedia: Duck Migration and Food Sources", "Birdwatcher Mike: Sayonara Biw");
            yield return Window.OpenMessage();
            switch (Window.SelectResult())
            {
                case 0:
                    yield return Window.Message("The time has come for the ducks to migrate once again. At record high temperatures this year the ducks’ season migration has been delayed by two and a half months. Their extended stay in the area has contributed to a decreasing population of local fish life in the Biwa-Kyoto region, and it’s estimated that this will continue to curb the population as environmental changes occur. The migration of animals continues to be a major concern for scientists as climate change affects the world.Ducks can predict the coming of major weather shifts, storms, and changes in the environment, and are a major source of information for scientists to combat and evaluate the climate situation.");
                    yield return Window.Select("Ducks", "Biwa");
                    switch (Window.SelectResult())
                    {
                        case 0:
                            yield return Window.Message("During migration season, ducks gather in large groups to embark on a journey as a simultaneous movement. This often occurs twice a year, between breeding and wintering grounds. The timing is affected by changes in day length, using celestial cues from the Sun and the stars, the Earth’s magnetic field, and the duck’s own mental maps. Ducks have a diverse diet ranging between seed, fish, aquatic plants, and invertebrates.When these sources face scarcity they are prompted to begin migration to warmer regions and more reliable sources.");
                            break;
                        case 1:
                            yield return Window.Message("Take-off! The birds are gone! The group of ducks I’ve been photographing for my lovely followers have just gone ahead. I arrived on the premise at around 5 am and found them in the air migrating to their next destination. Sayonara ducks!");
                            break;
                    }
                    break;

                case 1:
                    yield return Window.Message("During migration season, ducks gather in large groups to embark on a journey as a simultaneous movement. This often occurs twice a year, between breeding and wintering grounds. The timing is affected by changes in day length, using celestial cues from the Sun and the stars, the Earth’s magnetic field, and the duck’s own mental maps. Ducks have a diverse diet ranging between seed, fish, aquatic plants, and invertebrates.When these sources face scarcity they are prompted to begin migration to warmer regions and more reliable sources.");
                    break;

                case 2:
                    yield return Window.Message("Take-off! The birds are gone! The group of ducks I’ve been photographing for my lovely followers have just gone ahead. I arrived on the premise at around 5 am and found them in the air migrating to their next destination. Sayonara ducks!");
                    break;

            }

            yield return Window.CloseMessage();

            yield return Fader.YieldFadeOut(ConstDef.FADETIME);

            Main.Instance.SetMessageActive(Main.TypeMessage.Game);
        }
    }
}