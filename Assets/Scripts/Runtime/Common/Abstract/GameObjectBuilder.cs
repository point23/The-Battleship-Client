using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Runtime.Common.Interface;
using Runtime.Infrastructures.Helper;
using Runtime.Utilities;
using ThirdParty.SimpleJSON;
using UnityEngine;

namespace Runtime.Common.Abstract
{
    [RequireComponent(typeof(StringGameObjectDictionary))]
    public abstract class GameObjectBuilder : MonoBehaviour, ICommandResponder
    {
        public Transform layout;
        public StringGameObjectDictionary prefabs;
        public Dictionary<string, GameObject> instances;

        public void Awake()
        {
            instances = new Dictionary<string, GameObject>();
        }

        public UniTask Run(string action, JSONNode data)
        {
            switch (action)
            {
                case "Build":
                    Build(data);
                    break;
                case "Dispose":
                    Dispose(data);
                    break;
            }
            return UniTask.CompletedTask;
        }
        
        protected bool ContainsPrefab(string name)
        {
            return prefabs.ContainsKey(name);
        }
        
        protected bool ContainsInstance(string name)
        {
            return instances.ContainsKey(name);
        }
        
        protected void RegisterNewPrefabAs(string name, GameObject gameObject)
        {
            prefabs[name] = gameObject;
        }

        protected GameObject BuildAs(string name, string id)
        {
            var go = Instantiate(prefabs[name], layout);
            instances[id] = go;
            return go;
        }
        
        protected void DisposeInstance(string id)
        {
            Destroy(instances[id]);
        }

        protected virtual void PostDispose(JSONNode data) { }
        
        protected virtual void PostBuild(JSONNode data)
        {
            throw new System.NotImplementedException();
        }

        private void Dispose(JSONNode data)
        {
            if (!ContainsInstance(data["id"].Value)) 
                return;
            
            DisposeInstance(data["id"].Value);
            PostDispose(data);
        }
        
        private void Build(JSONNode data)
        {
            DebugPG13.Log("data", data);
            if (!ContainsPrefab(data["name"].Value)) 
                return;
            
            PostBuild(data);
        }
    }
}