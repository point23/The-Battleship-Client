using System;
using UnityEngine;

namespace DataTypes
{
    [Serializable]
    public class GridData
    {
        public Coord pos;

        public GridData(Coord pos)
        {
            this.pos = pos;
        }
        
        public GridData(int x, int y)
        {
            pos = new Coord(x, y);
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}