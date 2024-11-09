using UnityEngine;

namespace App
{
    public class PlayBGM : MonoBehaviour
    {

        [SerializeField] SoundList.BGM _No = SoundList.BGM.NONE;

        void Start()
        {
            SoundManager.Instance.PlayBGM(_No);
        }

    }
}
