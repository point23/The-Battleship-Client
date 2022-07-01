using System;
using UnityEngine;

namespace DataTypes
{
    [Serializable]
    public class GridData
    {
        public Coord coord;

        public GridData(Coord coord)
        {
            this.coord = coord;
        }
        
        public GridData(int x, int y)
        {
            coord = new Coord(x, y);
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}