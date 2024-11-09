using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace App
{
    public class PopupDialogMsg : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Button btnClose = null;
        [SerializeField] private TMP_Text txtMessage = null;

        [SerializeField] private SoundList.SE seClicked = SoundList.SE.OPEN_TAB;
        private CanvasGroup _canvasGroup = null;
        private bool isClicked = false;
        
        public UnityEngine.Events.UnityEvent onClosed = new UnityEvent();
        
        /// <summary>
        /// メッセージ
        /// </summary>
        public void SetMessage(string txtMessage, UnityAction onCloseFunc)
        {
            this.txtMessage.text = txtMessage;
            
            if (onCloseFunc != null)
            {
                onClosed.AddListener(onCloseFunc);
            }
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
            
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1.0f, 0.2f);
        }

        // Update is called once per frame
        void Update()
        {

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
                    Destroy(this.gameObject);
                });
        }
    }
}