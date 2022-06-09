using System;
using UnityEngine;

namespace DataTypes
{
    [Serializable]
    public class GridData
    {
        public Vector2Int Pos;

        public GridData(Vector2Int pos)
        {
            Pos = pos;
        }
        
        public GridData(int x, int y)
        {
            Pos = new Vector2Int(x, y);
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}