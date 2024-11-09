using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

namespace App
{
    public class PopupDialogYesNo : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Button btnYes = null;
        [SerializeField] private UnityEngine.UI.Button btnNo = null;
        [SerializeField] private TMP_Text txtYes = null;
        [SerializeField] private TMP_Text txtNo = null;
        [SerializeField] private TMP_Text txtMessage = null;
        [SerializeField] private SoundList.SE seClicked = SoundList.SE.OPEN_TAB;

        private CanvasGroup _canvasGroup = null;
        private bool isClicked = false;

        public eYesNo Result = eYesNo.NO;
        
        public UnityEngine.Events.UnityEvent onClosed = new UnityEvent();

        private bool _is_end = false;
        public bool IsEnd { get => _is_end; }

        private int _selected = -1;
        public int Selected { get => _selected; }

        /// <summary>
        /// メッセージ
        /// </summary>
        public void SetMessage(string msg, string mYes, string mNo, UnityAction onCloseFunc)
        {
            this.txtMessage.text = msg;
            this.txtYes.text = mYes;
            this.txtNo.text = mNo;
            
            if (onCloseFunc != null)
            {
                onClosed.AddListener(onCloseFunc);
            }
        }
        
        // Start is called before the first frame update
        void Start()
        {
            UnityEngine.Debug.Assert(btnYes != null);
            UnityEngine.Debug.Assert(btnNo != null);
            UnityEngine.Debug.Assert(txtMessage != null);

            btnYes.interactable = true;
            btnYes.onClick.AddListener(() =>
            {
                if (isClicked == false)
                {
                    isClicked = true;
                    Result = eYesNo.YES;
                    App.SoundManager.Instance.PlaySE(seClicked);
                    Close();
                }
            });
            
            btnNo.interactable = true;
            btnNo.onClick.AddListener(() =>
            {
                if (isClicked == false)
                {
                    isClicked = true;
                    Result = eYesNo.NO;
                    App.SoundManager.Instance.PlaySE(seClicked);
                    Close();
                }
            });
            
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1.0f, 0.2f);
        }

        void Close()
        {
            // ウィンドウを消す
            btnYes.interactable = false;
            btnNo.interactable = false;
            _canvasGroup.DOFade(0.0f, 0.2f)
                .OnComplete(() =>
                {
                    _selected = (int) Result;
                    onClosed.Invoke();
                    onClosed.RemoveAllListeners();
                    _is_end = true;
                    Destroy(this.gameObject);
                });
        }
        
    }
}
