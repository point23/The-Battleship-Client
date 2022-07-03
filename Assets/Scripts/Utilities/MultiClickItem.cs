using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Button = UnityEngine.UI.Button;

namespace Utilities
{
    [Serializable]
    public class MultiClickItem : MonoBehaviour, IPointerEnterHandler
    {
        public bool isActive;
        public bool isEnabled;
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                if (!isActive)
                    return;
                isEnabled = value;
            }
        }
        
        [HideInInspector] public UnityEvent<MultiClickItem> multiClickedEvent;
        [HideInInspector] public UnityEvent<MultiClickItem> clickStartEvent;
        [HideInInspector] public UnityEvent<MultiClickItem> clickEndEvent;
        private int _counter;
        private float _lastClickTime;
        private int _maxCount;
        private float _interval;

        public void Awake()
        {
            ResetCounter();
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        public void Init(int maxCount, float interval)
        {
            _maxCount = maxCount;
            _interval = interval;
        }

        private void OnClick()
        {
            if (!isActive || !IsEnabled)
                return;
            
            _counter += 1;
            if (Time.time - _lastClickTime >= _interval)
            {
                ResetCounter();
            }
            if (_counter != _maxCount) return;
            
            multiClickedEvent.Invoke(this);
            ResetCounter();
        }

        private void ResetCounter()
        {
            _counter = 1;
            _lastClickTime = Time.time;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isActive || !IsEnabled)
                return;

            ToolTipHandler.instance.Generate("rotate", transform);
        }
    }
}