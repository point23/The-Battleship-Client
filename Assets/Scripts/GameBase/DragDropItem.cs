using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Utilities;

namespace GameBase
{
    [Serializable]
    public class DragDropItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public bool isActive;
        public bool isEnabled;

        public UnityEvent<Vector2Int> onDraggedEvent = new UnityEvent<Vector2Int>();
        public UnityEvent<DragDropItem> onBeginDragEvent = new UnityEvent<DragDropItem>();
        public UnityEvent<DragDropItem> onEndDragEvent = new UnityEvent<DragDropItem>();
        
        public bool IsEnabled
        {
            get => isEnabled;
            // todo: only used in test mode
            set
            {
                if (!isActive)
                    return;
                
                GetComponent<CanvasGroup>().alpha = value ? 1f : 0.8f;
                isEnabled = value;
            }
        }

        private Diastimeter _diastimeter = new Diastimeter();
        
        public void Init(Diastimeter diastimeter)
        {
            _diastimeter = new Diastimeter(diastimeter);
            IsEnabled = true;
        }
        
        public void OnPointerDown(PointerEventData eventData) { }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!isActive || !IsEnabled)
                return;
            _diastimeter.Begin(eventData.position);
            onBeginDragEvent.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!isActive || !IsEnabled)
                return;
            // DebugPG13.Log("GameObject Name", gameObject.name);
            _diastimeter.End();
            onEndDragEvent.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isActive || !IsEnabled)
                return;
            
            var delta = _diastimeter.CalculateDelta(eventData.position); 
            if (!_diastimeter.IsAvailable || delta.sqrMagnitude <= 0)
                return;
            onDraggedEvent.Invoke(delta);
            _diastimeter.End();
            _diastimeter.Begin(eventData.position);
        }
    }
}
