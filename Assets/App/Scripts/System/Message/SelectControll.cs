using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App
{
    public class SelectControll : MonoBehaviour
    {
        [SerializeField] private SoundList.SE clickSE = SoundList.SE.NONE;
        [SerializeField] public RectTransform centerGui;
        [SerializeField] public Image imageSelect;
        [SerializeField] private Scrollbar scrollbar = null;

        private List<SelectItem> list = new List<SelectItem>();
        private bool hasBegun = false;
        private bool isSelected = false;
        private int result = -1;
        private bool _is_btmScroll = false;
        private float _timer_btmScroll = 0;
        
        void Update()
        {
            if (_timer_btmScroll > 0.0f && scrollbar != null)
            {
                scrollbar.value = 0;
                _timer_btmScroll -= Time.deltaTime;
                if (_timer_btmScroll <= 0.0f)
                {
                    _timer_btmScroll = 0.0f;
                }
            }
        }

        public SelectItem AddItem(string text, Sprite sprIcon = null)
        {
            var objItem = GameObject.Instantiate(imageSelect, centerGui);
            objItem.gameObject.SetActive(true);

            var item = objItem.GetComponent<SelectItem>();
            item.SetText(this, text, sprIcon);
            item.selectNo = list.Count;
            // List
            list.Add(item);

            hasBegun = true;
            isSelected = false;
            result = -1;
            
            if (scrollbar != null)
            {
                scrollbar.value = 0;
                _timer_btmScroll = 1f;
            }

            return item;
        }

        public bool IsExceptSelection()
        {
            return (hasBegun == false);
        }

        public bool IsEndOfSelected()
        {
            return isSelected;
        }

        public int Result
        {
            get { return result; }
        }

        public void SelectedItem(SelectItem item)
        {
            isSelected = true;
            result = item.selectNo;
            //
            // // SE
            // App.SoundManager.Instance.PlaySE(clickSE);
        }

        public void Close()
        {
            foreach (var item in list)
            {
                item.Close();
            }

            list.Clear();

            hasBegun = false;
            isSelected = false;
        }

    }
}
