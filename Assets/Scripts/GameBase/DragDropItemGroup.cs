using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace GameBase
{
    public class DragDropItemGroup : MonoBehaviour
    {
        public float onDragScaleUp = 1.2f;
        public float onDragAlphaDelta = 0.8f;
        public UnityEvent<Vector2Int> onDraggedEvent;
        public List<DragDropItem> dragDropItems = new List<DragDropItem>();
        private CanvasGroup _canvasGroup;
        private Diastimeter _diastimeter;

        public void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        
        public void Init(Diastimeter diastimeter)
        {
            dragDropItems = GetComponentsInChildren<DragDropItem>().ToList();
            _diastimeter = diastimeter;
            foreach (var dragDropItem in dragDropItems)
            {
                dragDropItem.onDraggedEvent.AddListener(OnDragged);
                dragDropItem.onBeginDragEvent.AddListener(RenderBeginDrag);
                dragDropItem.onEndDragEvent.AddListener(RenderEndDrag);
                dragDropItem.Init(_diastimeter);
            }
            SetAllItems(true);
        }

        private void OnDragged(Vector2Int delta)
        {
            onDraggedEvent.Invoke(delta);
        }
        
        private void RenderBeginDrag(DragDropItem chosenOne)
        {
            SetAllItemsExcept(chosenOne, false);
            DebugPG13.Log("Enabled Item Num", CountEnabledItems());
            transform.localScale = Vector3.one * onDragScaleUp;
            _canvasGroup.alpha = onDragAlphaDelta;
        }
        
        private void RenderEndDrag(DragDropItem chosenOne)
        {
            SetAllItemsExcept(chosenOne, true);
            DebugPG13.Log("Enabled Item Num", CountEnabledItems());
            transform.localScale = Vector3.one;
            _canvasGroup.alpha = 1f;
        }

        private void SetAllItems(bool isEnabled)
        {
            foreach (var item in dragDropItems)
            {
                item.IsEnabled = isEnabled;
            }
        }
        
        private void SetAllItemsExcept(DragDropItem chosenOne, bool isEnabled)
        {
            foreach (var item in dragDropItems)
            {
                if (item != chosenOne)
                    item.IsEnabled = isEnabled;
            }
        }
        
        // todo: only used in test mode
        private int CountEnabledItems()
        {
            return dragDropItems.Sum(item => item.IsEnabled ? 1 : 0);
        }
    }
}