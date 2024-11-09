using UnityEngine;

namespace App
{
    public class ClickPlaySE : MonoBehaviour
    {
        [SerializeField] SoundList.SE _seNo = SoundList.SE.OPEN_TAB;

        void Start()
        {
            var button = GetComponent<UnityEngine.UI.Button>();
            if (button != null) {
                button.onClick.AddListener(() => {
                    // クリックされたらSEを鳴らす
                    SoundManager.Instance.PlaySE(_seNo);
                });
            }

        }
    }
}