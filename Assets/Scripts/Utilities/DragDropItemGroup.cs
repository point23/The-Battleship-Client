﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
    public class DragDropItemGroup : MonoBehaviour
    {
        public float onDragScaleUp = 1.2f;
        public float onDragAlphaDelta = 0.8f;
        public UnityEvent<Vector2Int> draggedEvent;
        public List<DragDropItem> dragDropItems = new List<DragDropItem>();
        private CanvasGroup _canvasGroup;
        private Diastimeter _diastimeter;
        // todo: refactor -> extract group-child items 
        private MultiClickItemGroup _multiClickItemGroup;

        public void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        
        public void Init(Diastimeter diastimeter, MultiClickItemGroup multiClickItemGroup)
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
            _multiClickItemGroup = multiClickItemGroup;
        }
        
        public void SetAllItems(bool isEnabled)
        {
            foreach (var item in dragDropItems)
            {
                item.IsEnabled = isEnabled;
            }
        }

        private void OnDragged(Vector2Int delta)
        {
            draggedEvent.Invoke(delta);
        }
        
        private void RenderBeginDrag(DragDropItem chosenOne)
        {
            SetAllItemsExcept(chosenOne, false);
            transform.localScale = Vector3.one * onDragScaleUp;
            _canvasGroup.alpha = onDragAlphaDelta;
            _multiClickItemGroup.SetAllItems(false);
        }
        
        private void RenderEndDrag(DragDropItem chosenOne)
        {
            SetAllItemsExcept(chosenOne, true);
            transform.localScale = Vector3.one;
            _canvasGroup.alpha = 1f;
            _multiClickItemGroup.SetAllItems(true);
        }

        private void SetAllItemsExcept(DragDropItem chosenOne, bool isEnabled)
        {
            foreach (var item in dragDropItems)
            {
                if (item != chosenOne)
                    item.IsEnabled = isEnabled;
            }
        }
    }
}