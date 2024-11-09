using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace App
{
    public class PopupDialogScrMsgs : MonoBehaviour 
    {
        [SerializeField] private UnityEngine.UI.Button btnClose = null;
        [SerializeField] private TMP_Text txtTitle = null;
        [SerializeField] private TMP_Text txtMessage = null;
        [SerializeField] private UnityEngine.UI.Button btnAgree = null;

        [SerializeField] private SoundList.SE seClicked = SoundList.SE.OPEN_TAB;
        private CanvasGroup _canvasGroup = null;
        private bool isClicked = false;
        
        public UnityEngine.Events.UnityEvent onClosed = new UnityEvent();

        private bool _is_end = false;
        public bool IsEnd
        {
            get => _is_end;
        }

        public int Selected
        {
            get => -1;
        }

        public string TextTitle
        {
            get => txtTitle.text;
            set => txtTitle.text = value;
        }

        public string TextMessage
        {
            get => txtMessage.text;
            set => txtMessage.text = value;
        }

        private bool _is_show_agree = false;

        public bool IsShowAgreeButton
        {
            get => _is_show_agree;
            set => _is_show_agree = value;
        }

        private bool _is_center_text = false;

        public bool IsCenterText
        {
            get => _is_center_text;
            set => _is_center_text = value;
        }


        // Start is called before the first frame update
        void Start()
        {
            UnityEngine.Debug.Assert(btnClose != null);
            UnityEngine.Debug.Assert(txtMessage != null);

            btnClose.interactable = true;
            btnClose.onClick.AddListener(() =>
            {
                if (isClicked == false)
                {
                    isClicked = true;
                    App.SoundManager.Instance.PlaySE(seClicked);
                    Close();
                }
            });

            if (_is_show_agree == true)
            {
                // 同意ボタンをつけるかどうか・・検討中
            }

            if (_is_center_text == true)
            {
                txtMessage.alignment = TextAlignmentOptions.Top; //TextAnchor.UpperCenter;
            }
            else
            {
                txtMessage.alignment = TextAlignmentOptions.TopLeft; //TextAnchor.UpperLeft;
            }
            
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1.0f, 0.2f);
        }
        
        void Close()
        {
            // ウィンドウを消す
            btnClose.interactable = false;
            _canvasGroup.DOFade(0.0f, ConstDef.FADETIME)
                .OnComplete(() =>
                {
                    onClosed.Invoke();
                    onClosed.RemoveAllListeners();
                    _is_end = true;
                    Destroy(this.gameObject);
                });
        }
    }
}