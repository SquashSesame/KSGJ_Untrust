using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace App
{
    public class MessageControll : MonoBehaviour
    {
        [SerializeField] private Main.TypeMessage CurrentType;
        [SerializeField] public SelectControll SelectCtrl;
        [SerializeField] private TMP_Text textName;
        [SerializeField] private CanvasGroup cvgpName;
        [SerializeField] public CanvasGroup cvgpMessageWithFace;
        [SerializeField] private CanvasGroup cvgpMessage;
        [SerializeField] private TMP_Text textAreaWithFace;
        [SerializeField] private UnityEngine.UI.Image imgFace;

        [SerializeField] private CanvasGroup cgTelop = null;
        [SerializeField] private TMP_Text textMessage = null;
        [SerializeField] private UnityEngine.UI.Image imgTelop = null;

        [SerializeField] private smartphone btnSmartphone;
        
        static Color colWhiteAlpha0 = new Color(1, 1, 1, 1);
        static Color colBlackAlpha0 = new Color(0, 0, 0, 1);
        private const float defFadeTime = ConstDef.FADETIME;

        private const float SELECTITEM_HEIGHT = 110;
        private const char KEYSTOP_CODE0 = '@';
        private const char KEYSTOP_CODE1 = '＠';

        string strMessage;
        private float timeCharDisplay = 0.1f;

        private int topMessage;
        private int idxMessage;
        private int cntMessage;
        private float waitMessage;
        private char lastChar;
        
        private Color colZero = new Color(0, 0, 0, 0);

        public UnityEngine.UI.Image ImageFace
        {
            get => imgFace;
        }

        // 終了判定
        public bool IsEndOfMessage
        {
            get { return (topMessage + idxMessage) >= cntMessage; }
        }

        void Awake()
        {
            // Initialize
            Debug.Assert(textAreaWithFace != null);
            Debug.Assert(cvgpMessage != null);

            cvgpMessage.alpha = 0;
            cvgpMessageWithFace.alpha = 1;
            SetMessageText(string.Empty);
        }

        IEnumerator YieldFadeAlpha(float srcVal, float dstVal, float time, CanvasGroup target, UnityAction funcEnd)
        {
            if (time > 0) {
                float _time = 0;
                while (_time < time) {
                    _time += Time.deltaTime;
                    if (_time >= time) {
                        _time = time;
                        if (funcEnd != null) {
                            funcEnd.Invoke();
                        }
                    }

                    target.alpha = Mathf.Lerp(srcVal, dstVal, _time / time);
                    yield return null;
                }
            }
            else {
                target.alpha = dstVal;
            }

            yield return null;
        }

        #region Smartphone

        public void SetActiveSmartphoneButton(bool isActive)
        {
            btnSmartphone.gameObject.SetActive(isActive);
        }
        
        #endregion
        
        
        #region Message
        
        public IEnumerator OpenMessage()
        {
            cvgpMessageWithFace.alpha = 0;
            cvgpMessageWithFace.gameObject.SetActive(true);
            yield return YieldFadeAlpha(0, 1, defFadeTime, cvgpMessageWithFace, () => { });
        }

        public IEnumerator CloseMessage()
        {
            yield return YieldFadeAlpha(1, 0, defFadeTime, cvgpMessageWithFace, () => { });
            ForceClose();
            yield return null;
        }

        public IEnumerator Message(string message)
        {
            if (cvgpMessageWithFace.alpha == 0) {
                yield return OpenMessage();
            }
            
            yield return Message(message, "", true);
        }
        
        public IEnumerator Message(string message, bool isWait = true)
        {
            yield return Message(message, "", isWait);
        }
        
        public IEnumerator Message(string message, string name = "", bool isWait=true)
        {
            // 名前エリア
            if (textName != null) {
                textName.text = name;
                cvgpName.alpha = (string.IsNullOrEmpty(name)) ? 0 : 1;
            }

            // メッセージエリア
            SetMessageText(string.Empty);
            strMessage = message;
            topMessage = 0;
            idxMessage = 0;
            cntMessage = message.Length;
            waitMessage = 0.0f;
            lastChar = ' ';

            if (isWait) {
                yield return WaitTouch();
                SetMessageText(string.Empty);
            }

            yield return null;
        }
        
        public void ForceClose()
        {
            cvgpMessageWithFace.alpha = 0;
            cvgpMessageWithFace.gameObject.SetActive(false);
            ClearMessage();
        }

        public void ClearMessage()
        {
            SetMessageText("");
        }
        
        #endregion

        #region Telop
        
        public bool IsTeloping {
            get => cgTelop.gameObject.activeSelf;
        }
        
        public IEnumerator OpenTelop(string message, float time=ConstDef.FADETIME)
        {
            imgTelop.color = colBlackAlpha0;
            textMessage.color = Color.white;
            textMessage.text = message;
            cgTelop.gameObject.SetActive(true);
            yield return YieldFadeAlpha(0, 1, time, cgTelop, () => { });
        }

        public IEnumerator CloseTelop()
        {
            cgTelop.alpha = 1;
            imgTelop.color = colBlackAlpha0;
            textMessage.color = Color.white;
            yield return YieldFadeAlpha(1, 0, defFadeTime, cgTelop, () => { });
            cgTelop.gameObject.SetActive(false);
            textMessage.text = "";
            yield return null;
        }
        
        #endregion
        
        #region Select

        public IEnumerator Select(params string[] args)
        {
            SelectCtrl.Close();
            
            float itemHeight = SELECTITEM_HEIGHT * (float)args.Length * 0.5f;
            
            foreach (var it in args) {
                var item = SelectCtrl.AddItem(it);
                item.gameObject.transform.localPosition = new Vector3(0, itemHeight, 0);
                itemHeight -= SELECTITEM_HEIGHT;
            }

            while (SelectCtrl.IsEndOfSelected() == false) {
                yield return null;
            }
            
            SelectCtrl.Close();
            yield return null;
        }
        
        public int SelectResult()
        {
            return SelectCtrl.Result;
        }
        
        #endregion
        
        public IEnumerator WaitTouch()
        {
            GameMain.Instance.TouchRequest();
            while (GameMain.Instance.IsTouched() == false && Main.Instance.ActiveMessage == CurrentType)
            {
                yield return null;
            }
            GameMain.Instance.DestoryTouchIcon();
        }
        
        void SetMessageText(string message)
        {
            textAreaWithFace.text = message;
        }

        // Update is called once per frame
        void Update()
        {
            if (Main.Instance.ActiveMessage == CurrentType) {
                  UpdateMessageControll();
            }
        }
        
        void UpdateMessageControll()
        {
            if (IsEndOfMessage == false && Main.Instance.ActiveMessage == CurrentType)
            {
                switch (lastChar)
                {
                    case KEYSTOP_CODE0:
                    case KEYSTOP_CODE1:
                        // ボタンが押されるまで待つ
                        GameMain.Instance.TouchRequest();
                        if (GameMain.Instance.IsTouched()
                            && (SelectCtrl == null || SelectCtrl.IsExceptSelection() == true))
                        {
                            GameMain.Instance.DestoryTouchIcon();
                            // 改行送り
                            topMessage += idxMessage + 1;
                            idxMessage = 1;
                            SetMessageText(string.Empty);
                            if (IsEndOfMessage == false)
                            {
                                UpdateMessage();
                            }
                        }

                        break;

                    default:
                        // クリックされたら全表示
                        if (GameMain.Instance.IsTouched()
                            && (SelectCtrl == null || SelectCtrl.IsExceptSelection() == true))
                        {
                            // 次の改行または文の終了まで
                            while ((topMessage + idxMessage + 1) < cntMessage
                                   && this.strMessage[topMessage + idxMessage + 1] != '\n'
                                   && this.strMessage[topMessage + idxMessage + 1] != KEYSTOP_CODE0
                                   && this.strMessage[topMessage + idxMessage + 1] != KEYSTOP_CODE1)
                            {
                                idxMessage++;
                            }

                            waitMessage = timeCharDisplay;
                        }

                        // １文字表示する
                        waitMessage += Time.deltaTime;
                        if (waitMessage >= timeCharDisplay)
                        {
                            waitMessage = 0.0f;
                            UpdateMessage();
                            if (lastChar != KEYSTOP_CODE0 && lastChar != KEYSTOP_CODE1)
                            {
                                idxMessage++;
                            }
                        }

                        break;
                }
            }
        }

        void UpdateMessage()
        {
            lastChar = this.strMessage[topMessage + idxMessage];
            if (lastChar != KEYSTOP_CODE0 && lastChar != KEYSTOP_CODE1)
            {
                SetMessageText(this.strMessage.Substring(topMessage, idxMessage + 1));
            }
        }

        public void SetFaceImage(Sprite faceSprite, bool flipX = false)
        {
            if (imgFace != null)
            {
                // 左右フリップ
                var scale = imgFace.gameObject.transform.localScale;
                float size = Mathf.Abs(scale.x);
                scale.x = (flipX) ? -size : size;
                imgFace.gameObject.transform.localScale = scale;

                // 顔グラ表示・非表示
                if (faceSprite != null)
                {
                    imgFace.sprite = faceSprite;
                    imgFace.color = Color.white;
                }
                else
                {
                    cvgpName.alpha = 0;
                    imgFace.sprite = null;
                    imgFace.color = colZero;
                }
            }
        }
    }
}