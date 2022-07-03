using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
    public class MultiClickItemGroup : MonoBehaviour
    {
        [Range(1,4)]public int maxCount;
        [Range(0.5f, 1.5f)]public float interval;
        [HideInInspector] public UnityEvent<MultiClickItem> multiClickedEvent;
        [HideInInspector] public List<MultiClickItem> multiClickItems;
        private DragDropItemGroup _dragDropItemGroup;
        public void Init(DragDropItemGroup dragDropItemGroup)
        {
            multiClickItems = GetComponentsInChildren<MultiClickItem>().ToList();
            foreach (var multiClickItem in multiClickItems)
            {
                multiClickItem.multiClickedEvent.AddListener(OnMultiClicked);
                multiClickItem.clickStartEvent.AddListener(OnClickStart);
                multiClickItem.clickEndEvent.AddListener(OnClickEnd);
                multiClickItem.Init(maxCount, interval);
            }
            SetAllItems(true);
            _dragDropItemGroup = dragDropItemGroup;
        }

        public void SetAllItems(bool isEnabled)
        {
            foreach (var item in multiClickItems)
            {
                item.IsEnabled = isEnabled;
            }
        }
        
        private void OnMultiClicked(MultiClickItem chosenOne)
        {
            multiClickedEvent.Invoke(chosenOne);
        }

        private void OnClickStart(MultiClickItem chosenOne)
        {
            SetAllItemsExcept(chosenOne, false);
            _dragDropItemGroup.SetAllItems(false);
        }
        
        private void OnClickEnd(MultiClickItem chosenOne)
        {
            SetAllItemsExcept(chosenOne, true);
            _dragDropItemGroup.SetAllItems(true);
        }

        private void SetAllItemsExcept(MultiClickItem chosenOne, bool isEnabled)
        {
            foreach (var item in multiClickItems)
            {
                if (item != chosenOne)
                    item.IsEnabled = isEnabled;
            }
        }
    }
}