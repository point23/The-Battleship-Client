using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Runtime.Common.Interface;
using Runtime.Infrastructures.Helper;
using ThirdParty.SimpleJSON;
using UnityEngine;

namespace Runtime.Common.Responders
{
    public class DataSource : ICommandResponder
    {
        public Dictionary<string, GameObject> gameObjectsDictionary;
        public Dictionary<string, string> linksDictionary;
        public Dictionary<string, JSONNode> assetsDictionary;

        public DataSource()
        {
            linksDictionary = new Dictionary<string, string>();
            assetsDictionary = new Dictionary<string, JSONNode>();
            gameObjectsDictionary = new Dictionary<string, GameObject>();
        }

        public void AddNewAsset(string id, JSONNode data)
        {
            assetsDictionary.Add(id, data);
        }

        public void AddNewGameObject(string id, GameObject go)
        {
            gameObjectsDictionary.Add(id, go);
        }
        
        public GameObject TryGetGameObject(string id)
        {
            return gameObjectsDictionary[id];
        }

        public bool ContainsAsset(string id)
        {
            return assetsDictionary.ContainsKey(id);
        }

        public JSONNode TryGetAsset(string name)
        {
            assetsDictionary.TryGetValue(name, out var value);
            if (value == null)
            {
                // raise error
            }
            return value;
        }

        public T TryGetAsset<T>(string name) where T : new()
        {
            var value = TryGetAsset(name);
            return JsonUtility.FromJson<T>(value.Value);
        } 

        public UniTask Run(string action, JSONNode data)
        {
            DebugPG13.Log(new Dictionary<object, object>
            {
                {"action", action},
                {"data", data}
            });
            switch (action)
            {
                case "RegisterUri": 
                    RegisterUri(data);
                    break;
            }
            return UniTask.CompletedTask;
        }
        
        private void RegisterUri(JSONNode data)
        {
           foreach (var source in data.Children)
           {
               linksDictionary.Add(source["rel"].Value, source["href"].Value);
           }
        }
    }
}