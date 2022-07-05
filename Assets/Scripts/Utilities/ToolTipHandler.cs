using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DataTypes;
using UnityEngine;

namespace Utilities
{
    public class ToolTipHandler : MonoBehaviour
    {
        public static ToolTipHandler instance;
        public StringGameObjectDictionary toolTips;
        public Transform tipsLayout;
        public bool shouldGenerating;
        private bool _isGenerating;
        
        public void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(instance);
            }
            else
            {
                instance = this;
            }

            _isGenerating = false;
        }

        public async void Generate(string name, Vector3 pos)
        {
            if (!shouldGenerating || !toolTips.ContainsKey(name)) return;
            
            var tip = toolTips[name];
            var go = Instantiate(tip, tipsLayout);
            go.transform.position = pos;
            await UniTask.Delay(1000);
            Destroy(go);
        }
        
        public async void Generate(string name, Transform trans)
        {
            if (!shouldGenerating || !toolTips.ContainsKey(name) || _isGenerating) return;

            _isGenerating = true;
            var tip = toolTips[name];
            var go = Instantiate(tip, trans);
            await UniTask.Delay(1000);
            Destroy(go);
            _isGenerating = false;
        }

        public bool Contains(string key)
        {
            return toolTips.ContainsKey(key);
        }
    }
}