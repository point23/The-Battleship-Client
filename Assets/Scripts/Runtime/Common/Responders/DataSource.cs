using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Runtime.Common.Interface;
using Runtime.Infrastructures.Helper;
using Runtime.Infrastructures.JSON;
using UnityEngine;

namespace Runtime.Common.Responders
{
    public class DataSource : ICommandResponder
    {
        public Dictionary<string, string> linksDictionary;
        public Dictionary<string, JsonData> assetsDictionary;

        public DataSource()
        {
            linksDictionary = new Dictionary<string, string>();
            assetsDictionary = new Dictionary<string, JsonData>();
        }

        public void Add(string name, JsonData data)
        {
            assetsDictionary.Add(name, data);
        }
        
        public bool Contains(string name)
        {
            return assetsDictionary.ContainsKey(name);
        }

        public (bool, JsonData) TryGetData(string name)
        {
            assetsDictionary.TryGetValue(name, out var value);
            return (value != null, value);
        }

        public UniTask Run(string action, JsonData data)
        {
            Debug.Log(data); 
            GetType().GetMethod(action)?.Invoke(this, new object[] { data });
            return UniTask.CompletedTask;
        }
        
        private void RegisterBasicUri(JsonData data)
        {
            DebugPG13.Log("uri count", data["uris"]);
           foreach (var source in data["uris"].Children)
           {
               linksDictionary.Add(source["rel"].Value, source["href"].Value);
           }
        }
    }
}