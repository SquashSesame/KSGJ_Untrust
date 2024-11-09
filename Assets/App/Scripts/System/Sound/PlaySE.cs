using UnityEngine;

namespace App
{
    public class PlaySE : MonoBehaviour
    {
        [SerializeField] SoundList.SE seNO = SoundList.SE.NONE;

        void Start()
        {
            SoundManager.Instance.PlaySE(seNO);
        }
    }
}
