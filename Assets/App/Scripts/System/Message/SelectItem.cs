using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace App
{
    public class SelectItem : MonoBehaviour
    {
        [SerializeField] private float closeTime = 0.2f;
        [SerializeField] public int selectNo;
        [SerializeField] public TMP_Text textItem;
        [SerializeField] UnityEngine.UI.Image imgIcon = null;

        private SelectControll parentCtrl;
        UnityEngine.UI.Button button = null;

        // Use this for initialization
        void Start()
        {
            Debug.Assert(textItem != null);

            button = GetComponent<UnityEngine.UI.Button>();
            Debug.Assert(button != null);

            button.onClick.AddListener(() =>
            {
                OnSelected();
            });
            transform.localScale = new Vector3(0, 1, 1);
            // Animation
            transform.DOScaleX(1, closeTime)
                .SetEase(Ease.InQuart);
        }

        public void OnSelected()
        {
            parentCtrl.SelectedItem(this);
        }

        public void Close()
        {
            transform.DOScaleY(0, closeTime)
                .SetEase(Ease.InQuart)
                .OnComplete(() =>
                {
                    Destroy(this.gameObject);
                });
        }

        public void SetText(SelectControll parent, string msg, Sprite sprIcon = null)
        {
            this.parentCtrl = parent;
            if (textItem != null)
            {
                textItem.text = msg;
            }

            if (imgIcon != null) {
                if (sprIcon != null) {
                    imgIcon.enabled = true;
                    imgIcon.sprite = sprIcon;
                }
                else {
                    imgIcon.enabled = false;
                }
            }
        }
    }
}