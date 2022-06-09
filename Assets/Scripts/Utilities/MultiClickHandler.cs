using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Utilities
{
    public class MultiClickHandler : MonoBehaviour
    {
        [HideInInspector]
        public UnityEvent onMultiClickedEvent; 
        
        [Range(1,4)]public int maxCount;
        [Range(0.5f, 1.5f)]public float interval;
        
        private int counter;
        private float lastClickTime;
        public void Awake()
        {
            Reset();
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            counter += 1;
            if (Time.time - lastClickTime >= interval)
            {
                Reset();
            }
            if (counter != maxCount) return;
            
            onMultiClickedEvent.Invoke();
            Reset();
        }

        private void Reset()
        {
            counter = 1;
            lastClickTime = Time.time;
        }
    }
}