using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App
{
    public class SelectControll : SingletonDontDestroy<SelectControll>
    {
        [SerializeField] private SoundList.SE clickSE = SoundList.SE.NONE;
        [SerializeField] public RectTransform centerGui;
        [SerializeField] public UnityEngine.UI.Image imageSelect;
        [SerializeField] private UnityEngine.UI.Scrollbar scrollbar = null;

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

        public void AddItem(string text, Sprite sprIcon = null)
        {
            var obj = GameObject.Instantiate(imageSelect, centerGui);
            obj.gameObject.SetActive(true);

            var item = obj.GetComponent<SelectItem>();
            item.SetText(text, sprIcon);
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

            // SE
            App.SoundManager.Instance.PlaySE(clickSE);
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
