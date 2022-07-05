using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Utilities
{
    [Serializable]
    public class DragDropItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler
    {
        public bool isActive;
        public bool isEnabled;

        public UnityEvent<Vector2Int> onDraggedEvent = new UnityEvent<Vector2Int>();
        public UnityEvent<DragDropItem> onBeginDragEvent = new UnityEvent<DragDropItem>();
        public UnityEvent<DragDropItem> onEndDragEvent = new UnityEvent<DragDropItem>();
        
        public bool IsActive
        {
            get => isActive;
            set
            {
                isActive = value;
                GetComponent<CanvasGroup>().blocksRaycasts = isActive;
            }
        }
        
        public bool IsEnabled
        {
            get => isEnabled;
            // todo: only used in test mode
            set
            {
                if (!isActive)
                    return;
                
                GetComponent<CanvasGroup>().alpha = value ? 1f : 0.6f;
                isEnabled = value;
            }
        }

        private Diastimeter _diastimeter = new Diastimeter();
        
        public void Init(Diastimeter diastimeter)
        {
            _diastimeter = new Diastimeter(diastimeter);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!IsActive || !IsEnabled)
                return;
            
            _diastimeter.Begin(eventData.position);
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!IsActive || !IsEnabled)
                return;
            
            onBeginDragEvent.Invoke(this);
        }
  
        public void OnEndDrag(PointerEventData eventData)
        {
            if (!IsActive || !IsEnabled)
                return;
            _diastimeter.End();
            onEndDragEvent.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!IsActive || !IsEnabled)
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
