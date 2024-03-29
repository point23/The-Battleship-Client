﻿using System.Collections.Generic;
using Runtime.Infrastructures.Helper;
using ThirdParty.SimpleJSON;
using UnityEngine;

namespace Runtime.GameBase
{
    public class BoardData
    {
        public string id;
        public Vector2 cellSize;
        public int rows;
        public int cols;
        public bool isInteractable;
        public List<int> unoccupiedGrids;

        public static JSONNode DefaultJsonData => JSONNode.Parse("{\"id\":\"null\", \"cellSize\":{\"x\":100,\"y\":100},\"rows\":9,\"cols\":9,\"isInteractable\":\"true\",\"unoccupiedGrids\":[]}");

        public static BoardData Default => new BoardData(DefaultJsonData);

        public BoardData(JSONNode data)
        {
            DebugPG13.Log("board id", data["id"]);
            id = data["id"].Value;
            cellSize = new Vector2(data["cellSize"]["x"].AsFloat, data["cellSize"]["y"].AsFloat);
            rows = data["rows"].AsInt;
            cols = data["cols"].AsInt;
            isInteractable = data["isInteractable"].AsBool;
            unoccupiedGrids = JsonUtility.FromJson<List<int>>(data["unoccupiedGrids"]);
        }

        public static JSONNode DefaultJsonDataWithId(string id)
        {
            var data = DefaultJsonData;
            data["id"].Value = id;
            return data;
        } 
    }
}