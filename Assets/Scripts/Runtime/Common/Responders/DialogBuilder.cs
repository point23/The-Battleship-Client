using System.Linq;
using Cysharp.Threading.Tasks;
using Runtime.Common.Interface;
using Runtime.Infrastructures.Helper;
using Runtime.Utilities;
using ThirdParty.SimpleJSON;
using UnityEditor;
using UnityEngine;

namespace Runtime.Common.Responders
{
    public class DialogBuilder : MonoBehaviour, ICommandResponder
    {
        public StringGameObjectDictionary dialogsPrefabs;
        public StringGameObjectDictionary instantiatedDialogs;
        public Transform layout;

        public UniTask Run(string action, JSONNode data)
        {
            switch (action)
            {
                case "Render": 
                    Render(data);
                    break;
                case "Dispose": 
                    Dispose(data);
                    break;
            }
            return UniTask.CompletedTask;
        }

        private void Render(JSONNode data)
        {
            DebugPG13.Log("data", data);
            if (!dialogsPrefabs.ContainsKey(data["name"].Value)) 
                return;
            var go = Instantiate(dialogsPrefabs[data["name"].Value], layout);
            instantiatedDialogs[data["id"].Value] = go;
        }

        private void Dispose(JSONNode data)
        {
            if (!instantiatedDialogs.ContainsKey(data["id"].Value)) return;
            
            Destroy(instantiatedDialogs[data["id"].Value]);
        }
    }
}