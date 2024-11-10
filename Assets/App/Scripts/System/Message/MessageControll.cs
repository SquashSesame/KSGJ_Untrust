using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace App
{
    public enum eWinMsg
    {
        Message,
        Log,
    }

    public class MessageControll : Singleton<MessageControll>
    {
        [SerializeField] private TMP_Text textName;
        [SerializeField] private CanvasGroup cvgpName;
        [SerializeField] private CanvasGroup cvgpMessageWithFace;
        [SerializeField] private CanvasGroup cvgpMessage;
        [SerializeField] private TMP_Text textAreaWithFace;
        [SerializeField] private TMP_Text textAreaLog;
        [SerializeField] private UnityEngine.UI.Image imgFace;
        [SerializeField] private float interpalTimeLog = 0.5f;

        [SerializeField] private CanvasGroup cgTelop = null;
        [SerializeField] private TMP_Text textMessage = null;
        [SerializeField] private UnityEngine.UI.Image imgTelop = null;

        static Color colWhiteAlpha0 = new Color(1, 1, 1, 1);
        static Color colBlackAlpha0 = new Color(0, 0, 0, 1);
        private const float fadeTime = ConstDef.FADETIME;

        private Queue<string> logQueue = new Queue<string>();

        private const char KEYSTOP_CODE0 = '@';
        private const char KEYSTOP_CODE1 = '＠';

        public string Message;
        private float timeCharDisplay = 0.1f;

        private int topMessage;
        private int idxMessage;
        private int cntMessage;
        private float waitMessage;
        private char lastChar;

        private bool is_log_message = false;
        private float timeLog = 0;

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
            // Debug.Assert(textName != null);
            // Debug.Assert(cvgpName != null);
            Debug.Assert(textAreaLog != null);
            Debug.Assert(textAreaWithFace != null);
            Debug.Assert(cvgpMessage != null);
            // Debug.Assert(cvgpMessageWithFace != null);

            SetSwitchWindow(eWinMsg.Message);
            SetMessage(string.Empty);
        }

        public IEnumerator YieldOpen()
        {
            cvgpMessageWithFace.alpha = 0;
            cvgpMessageWithFace.gameObject.SetActive(true);
            float _time = 0;
            while (_time < fadeTime)
            {
                _time += Time.deltaTime;
                if (_time >= fadeTime)
                {
                    _time = fadeTime;
                }
                cvgpMessageWithFace.alpha = Mathf.Lerp(0, 1, _time / fadeTime);
                yield return null;
            }
            yield return null;
        }

        public IEnumerator YieldClose()
        {
            float _time = 0;
            while (_time < fadeTime)
            {
                _time += Time.deltaTime;
                if (_time >= fadeTime)
                {
                    _time = fadeTime;
                }
                else
                {
                    cvgpMessageWithFace.alpha = Mathf.Lerp(1, 0, _time / fadeTime);
                }
                yield return null;
            }
            ForceClose();
            yield return null;
        }
        
        public void ForceClose()
        {
            cvgpMessageWithFace.alpha = 0;
            cvgpMessageWithFace.gameObject.SetActive(false);
        }

        public void ClearMessage()
        {
            SetMessage("");
        }

        public IEnumerator YieldOpenTelop(string message)
        {
            cgTelop.alpha = 0;
            imgTelop.color = colBlackAlpha0;
            textMessage.color = Color.white;
            textMessage.text = message;
            cgTelop.gameObject.SetActive(true);
            float _time = 0;
            while (_time < fadeTime)
            {
                _time += Time.deltaTime;
                if (_time >= fadeTime)
                {
                    _time = fadeTime;
                }
                cgTelop.alpha = Mathf.Lerp(0, 1, _time / fadeTime);
                yield return null;
            }
            yield return null;
        }

        public IEnumerator YieldCloseTelop()
        {
            cgTelop.alpha = 1;
            imgTelop.color = colBlackAlpha0;
            textMessage.color = Color.white;
            this.gameObject.SetActive(true);
            float _time = 0;
            while (_time < fadeTime)
            {
                _time += Time.deltaTime;
                if (_time >= fadeTime)
                {
                    _time = fadeTime;
                }
                cgTelop.alpha = Mathf.Lerp(1, 0, _time / fadeTime);
                yield return null;
            }
            cgTelop.gameObject.SetActive(false);
            textMessage.text = "";
            yield return null;
        }
        
        public IEnumerator YieldWaitTouch()
        {
            GameMain.Instance.TouchRequest();
            while (GameMain.Instance.IsTouched() == false)
            {
                yield return null;
            }
            GameMain.Instance.DestoryTouchIcon();
        }
        
        public void LogMessage(string log)
        {
            SetFaceImage(null);
            logQueue.Enqueue(log);
        }

        public void SetSwitchWindow(eWinMsg type)
        {
            if (cvgpName == null || cvgpMessageWithFace == null) return;
            
            is_log_message = type == eWinMsg.Log;
            if (is_log_message)
            {
                cvgpName.alpha = 0;
                // logMessage = string.Empty;
                cvgpMessage.alpha = 1;
                cvgpMessageWithFace.alpha = 0;
            }
            else
            {
                cvgpMessage.alpha = 0;
                cvgpMessageWithFace.alpha = 1;
            }
        }

        void SetMessageText(string message)
        {
            if (is_log_message)
            {
                // textAreaLog.text = message;
            }
            else
            {
                textAreaWithFace.text = message;
            }
        }

        // Update is called once per frame
        void Update()
        {
            UpdateLogControl();
            UpdateMessageControll();
        }

        void UpdateLogControl()
        {
            if (logQueue.Count > 0 && IsEndOfMessage == true)
            {
                timeLog += Time.deltaTime;
                if (timeLog >= interpalTimeLog)
                {
                    timeLog = 0;
                    SetSwitchWindow(eWinMsg.Log);
                    textAreaLog.text = logQueue.Dequeue();
                }
            }
        }

        void UpdateMessageControll()
        {
            if (IsEndOfMessage == false)
            {
                switch (lastChar)
                {
                    case KEYSTOP_CODE0:
                    case KEYSTOP_CODE1:
                        // ボタンが押されるまで待つ
                        GameMain.Instance.TouchRequest();
                        if (GameMain.Instance.IsTouched()
                            && (SelectControll.Instance == null || SelectControll.Instance.IsExceptSelection() == true))
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
                            && (SelectControll.Instance == null || SelectControll.Instance.IsExceptSelection() == true))
                        {
                            // 次の改行または文の終了まで
                            while ((topMessage + idxMessage + 1) < cntMessage
                                   && this.Message[topMessage + idxMessage + 1] != '\n'
                                   && this.Message[topMessage + idxMessage + 1] != KEYSTOP_CODE0
                                   && this.Message[topMessage + idxMessage + 1] != KEYSTOP_CODE1)
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
            lastChar = this.Message[topMessage + idxMessage];
            if (lastChar != KEYSTOP_CODE0 && lastChar != KEYSTOP_CODE1)
            {
                SetMessageText(this.Message.Substring(topMessage, idxMessage + 1));
            }
        }

        public void SetMessage(string message, string name = "")
        {
            SetSwitchWindow(eWinMsg.Message);
            SetMessageContent(message, name);
        }

        public void SetMessageContent(string message, string name = "")
        {
            // 名前エリア
            if (textName != null) {
                textName.text = name;
                cvgpName.alpha = (string.IsNullOrEmpty(name)) ? 0 : 1;
            }

            // メッセージエリア
            SetMessageText(string.Empty);
            this.Message = message;

            topMessage = 0;
            idxMessage = 0;
            cntMessage = message.Length;
            waitMessage = 0.0f;
            lastChar = ' ';
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