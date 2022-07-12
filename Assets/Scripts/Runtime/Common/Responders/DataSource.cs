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
        public Dictionary<string, string> linksDictionary;
        public Dictionary<string, JSONNode> assetsDictionary;

        public DataSource()
        {
            linksDictionary = new Dictionary<string, string>();
            assetsDictionary = new Dictionary<string, JSONNode>();
        }

        public void Add(string name, JSONNode data)
        {
            assetsDictionary.Add(name, data);
        }
        
        public bool Contains(string name)
        {
            return assetsDictionary.ContainsKey(name);
        }

        public JSONNode TryGetData(string name)
        {
            assetsDictionary.TryGetValue(name, out var value);
            if (value == null)
            {
                // raise error
            }
            return value;
        }

        public T TryGetData<T>(string name) where T : new()
        {
            var value = TryGetData(name);
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