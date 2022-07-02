using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Utilities;

namespace GameBase
{
    public class DragDropItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public float onDragScaleUp = 1.2f;
        public float onDragAlphaDelta = 0.6f;
        public UnityEvent<Vector2Int> onDraggedEvent;
        private CanvasGroup _canvasGroup;
        private Diastimeter _diastimeter;
        
        public void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Init(Diastimeter diastimeter)
        {
            _diastimeter = diastimeter;
        }
        
        public void OnPointerDown(PointerEventData eventData) { }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _diastimeter.Begin(eventData.position);
            RenderOnBeginDrag();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _diastimeter.End();
            RenderOnEndDrag();
        }

        public void OnDrag(PointerEventData eventData)
        {
            var delta = _diastimeter.CalculateDelta(eventData.position); 
            if (!_diastimeter.IsAvailable || delta.sqrMagnitude <= 0)
                return;
            onDraggedEvent.Invoke(delta);
            _diastimeter.End();
            _diastimeter.Begin(eventData.position);
        }

        private void RenderOnBeginDrag()
        {
            transform.localScale = new Vector3(onDragScaleUp, onDragScaleUp, onDragScaleUp);
            _canvasGroup.alpha = onDragAlphaDelta;
        }
        
        private void RenderOnEndDrag()
        {
            transform.localScale = Vector3.one;
            _canvasGroup.alpha = 1f;
        }
    }
}
