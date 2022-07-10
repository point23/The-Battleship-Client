using System.Data.SqlTypes;
using ThirdParty.SimpleJSON;
using UnityEngine;

namespace Runtime.Infrastructures.JSON
{
    public class JsonData : INullable
    {
        private string _data;
        private const string Empty = "{}";

        private static string HandleString(string data)
        {
            return data.Substring(1, data.Length-2);
        }
        
        public bool IsNull => _data == null;
        private JSONNode Data => JSONNode.Parse(_data);

        public string Value
        {
            get => JsonUtility.ToJson(_data) == Empty ? HandleString(_data) : Data.Value;
            private set => _data = value;
        }

        public JsonDataList Children => new(Data.AsArray);

        public JsonData()
        {
            _data = null;
        }

        public JsonData(string data)
        {
            _data = data;
            Debug.Log(JsonUtility.FromJson<string>(data));
        }

        public JsonData(JSONNode data)
        {
            _data = data.ToString();
        }

        // todo: catch error
        public JsonData this[string field] => new(Data[field]);

        public override string ToString()
        {
            return Value;
        }
    }
}