using UnityEngine;

namespace App
{
    public class PlayJingle : MonoBehaviour
    {
        [SerializeField] SoundList.JINGLE seNO = SoundList.JINGLE.NONE;

        void Start()
        {
            SoundManager.Instance.PlayJingle(seNO);
        }
    }
}
