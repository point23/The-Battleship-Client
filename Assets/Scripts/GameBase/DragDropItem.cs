using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Utilities;

namespace GameBase
{
    public class DragDropItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private GridsHandler GridsHandler => FindObjectOfType<GridsHandler>();
        private Transform _transform;
        private CanvasGroup _canvasGroup;
        private List<int> _occupiedGrids;
        private readonly Diastimeter _diastimeter = new Diastimeter();

        public UnityEvent<Vector2Int> OnDraggedEvent;
        
        public void Start()
        {
            _transform = GetComponent<Transform>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _diastimeter.Begin(eventData.position);
            _canvasGroup.alpha = 0.6f;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = 1f;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var delta = _diastimeter.End(eventData.position);
            var normalizedDelta = NormalizedDelta(delta);
            if (!TryMove(normalizedDelta)) return;
            
            OnDraggedEvent.Invoke(GridDelta(normalizedDelta));
            _diastimeter.Begin(eventData.position);
        }

        private bool TryMove(Vector2 delta)
        {
            _transform.localPosition += (Vector3) delta;
            return delta.sqrMagnitude > 0;
        }

        private Vector2Int GridDelta(Vector2 posDelta)
        { 
            var delta = posDelta / AppManager.Instance.CellSize;
            return new Vector2Int((int) delta.x, (int) delta.y);
        }
        
        private Vector2 NormalizedDelta(Vector2 delta)
        {
            var normalizedDelta = Vector2.zero;
            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                var gridSizeX = AppManager.Instance.CellSize.x;
                if (Mathf.Abs(delta.x) >= 0.5 * gridSizeX)
                {
                    normalizedDelta.x = (delta.x > 0)? gridSizeX : -gridSizeX;
                }
            }
            else if (Mathf.Abs(delta.y) > 0)
            {
                var gridSizeY = AppManager.Instance.CellSize.y;
                if (Mathf.Abs(delta.y) >= 0.5 * gridSizeY)
                {
                    normalizedDelta.y = (delta.y > 0)? gridSizeY : -gridSizeY;
                }
            }

            return normalizedDelta;
        }
    }
}
