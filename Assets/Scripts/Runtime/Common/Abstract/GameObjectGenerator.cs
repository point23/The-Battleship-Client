using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Runtime.Common.Interface;
using Runtime.Infrastructures.Helper;
using Runtime.Utilities;
using ThirdParty.SimpleJSON;
using UnityEngine;

namespace Runtime.Common.Abstract
{
    public abstract class GameObjectGenerator : MonoBehaviour, ICommandResponder
    {
        public GameObject prefab;
        public Transform layout;
        public Dictionary<string, GameObject> instances;

        public void Awake()
        {
            instances = new Dictionary<string, GameObject>();
        }

        public UniTask Run(string action, JSONNode data)
        {
            switch (action)
            {
                case "Generate":
                    Generate(data);
                    break;
                case "Dispose": 
                    Dispose(data);
                    break;
                case "DisposeAll":
                    DisposeAll();
                    break;
            }
            
            return UniTask.CompletedTask;
        }
        
        protected virtual void GenerateEach(JSONNode data)
        {
            throw new System.NotImplementedException();
        }
        
        protected virtual void PostGenerate(JSONNode data) { }

        protected virtual void PostDispose(JSONNode data) { }
        
        protected GameObject GenerateAs(string id)
        {
            var go = Instantiate(prefab, layout);
            instances[id] = go;
            return go;
        }

        private void DisposeTarget(string id)
        {
            Destroy(instances[id]);
        }
        
        private void Dispose(JSONNode data)
        {
            DisposeTarget(data["id"]);
            PostDispose(data);
        }

        private void Generate(JSONNode dataList)
        { 
            DebugPG13.Log("data list", dataList);
            foreach (var data in dataList.Children)
            {
                GenerateEach(data);
            }
            
        }
        
        private void DisposeAll()
        {
            foreach (var kvPair in instances)
            {
                // 
            }
        }

    }
}