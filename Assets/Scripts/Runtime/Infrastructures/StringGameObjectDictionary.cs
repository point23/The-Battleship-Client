using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Utilities
{
    [Serializable]
    public class StringGameObjectDictionary : MonoBehaviour
    {
        [SerializeField] public List<StringGameObjectPair> list;
        [HideInInspector] public Dictionary<string, GameObject> dict = new Dictionary<string, GameObject>();

        public void Awake()
        {
            dict.Clear();
            foreach (var pair in list)
            {
                dict[pair.name] = pair.gameObject;
            }
        }
        
        public GameObject this[string key]
        {
            get => dict[key];
            set => dict[key] = value;
        }
    
        public void OnAfterDeserialize()
        {
            list.Clear();
            foreach (var (key, value) in dict)
            {
                list.Add(new StringGameObjectPair(key, value));
            }        
        }

        public void OnBeforeSerialize()
        {
            dict.Clear();
            foreach (var pair in list)
            {
                dict[pair.name] = pair.gameObject;
            }
        }
        
        public bool ContainsKey(string key)
        {
            return dict.ContainsKey(key);
        }
    }

    [Serializable]
    public class StringGameObjectPair
    {
        public string name;
        public GameObject gameObject;

        public StringGameObjectPair(string name, GameObject gameObject)
        {
            this.name = name;
            this.gameObject = gameObject;
        }
    }
}