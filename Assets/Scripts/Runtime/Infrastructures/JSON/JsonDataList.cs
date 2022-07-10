using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Runtime.Common;
using Runtime.Infrastructures.Helper;
using ThirdParty.SimpleJSON;
using UnityEngine;

namespace Runtime.Infrastructures.JSON
{
    public class JsonDataList : IEnumerable<JsonData>
    {
        private readonly List<JsonData> _dataList;

        #region Properties
        public int Count => _dataList.Count;

        #endregion
        
        public JsonDataList(JSONArray array)
        {
            _dataList = new List<JsonData>();
            foreach (var data in array.Children)
            {
                _dataList.Add(new JsonData(data));
            }
        }
        
        public JsonDataList(string jsonString)
        {
            _dataList = new List<JsonData>();
            foreach (var data in JSONNode.Parse(jsonString).AsArray.Children)
            {
                _dataList.Add(new JsonData(data));
            }
        }

        public List<Command> ToCommands()
        {
            var list = new List<Command>();
            foreach (var data in _dataList)
            {                
                var command = new Command(data);
                list.Add(command);
            }
            return list;
        }
        
        public IEnumerator<JsonData> GetEnumerator()
        {
            return _dataList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}