using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace App
{
    public class SelectItem : MonoBehaviour
    {
        [SerializeField] private float closeTime = 0.2f;
        [SerializeField] public int selectNo;
        [SerializeField] public TMP_Text text;
        [SerializeField] UnityEngine.UI.Image imgIcon = null;

        UnityEngine.UI.Button button = null;

        // Use this for initialization
        void Start()
        {
            // Debug.Assert(controll != null);
            Debug.Assert(text != null);
            Debug.Assert(imgIcon != null);
            // animItem = GetComponent<Animator>();
            button = GetComponent<UnityEngine.UI.Button>();
            Debug.Assert(button != null);

            button.onClick.AddListener(() =>
            {
                OnSelected();
            });

            //transform.localScale = Vector3.zero;
            //transform.DOScale(Vector3.one, closeTime)
            //    .SetEase(Ease.InQuart);
        }

        public void OnSelected()
        {
            SelectControll.Instance.SelectedItem(this);
        }

        public void Close()
        {
            // 自動で削除される
            transform.DOScaleY(0, closeTime)
                .SetEase(Ease.InQuart)
                .OnComplete(() =>
                {
                    Destroy(this.gameObject);
                });
        }

        public void SetText(string msg, Sprite sprIcon = null)
        {
            if (text != null)
            {
                text.text = msg;
            }

            if (sprIcon != null)
            {
                imgIcon.enabled = true;
                imgIcon.sprite = sprIcon;
            }
            else
            {
                imgIcon.enabled = false;
            }
        }
    }
}