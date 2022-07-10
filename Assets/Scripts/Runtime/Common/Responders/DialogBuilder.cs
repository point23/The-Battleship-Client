using Cysharp.Threading.Tasks;
using Runtime.Common.Interface;
using Runtime.Infrastructures.Helper;
using Runtime.Infrastructures.JSON;
using Runtime.Utilities;
using UnityEditor;
using UnityEngine;

namespace Runtime.Common.Responders
{
    public class DialogBuilder : MonoBehaviour, ICommandResponder
    {
        public StringGameObjectDictionary dialogsPrefabs;
        public StringGameObjectDictionary instantiatedDialogs;
        public Transform layout;

        public UniTask Run(string action, JsonData data)
        {
            GetType().GetMethod(action)?.Invoke(this, new object[] { data });
            return UniTask.CompletedTask;
        }

        public void RenderNewDialog(JsonData data)
        {
            if (!dialogsPrefabs.ContainsKey(data["name"].Value)) return;
            
            var go = Instantiate(dialogsPrefabs[data["name"].Value], layout);
            instantiatedDialogs[data["id"].Value] = go;
        }
        
        public void DisposeExistedDialog(JsonData data)
        {
            if (!instantiatedDialogs.ContainsKey(data["id"].Value)) return;
            
            Destroy(instantiatedDialogs[data["id"].Value]);
        }
    }
}