using System;
using UnityEngine;

namespace DataTypes
{
    [Serializable]
    public class GridData
    {
        public Coord coord;
        public bool isOccupied;
        public bool isValid;

        public GridData(Coord coord, bool isOccupied = false, bool isValid = true)
        {
            this.coord = coord;
            this.isOccupied = isOccupied;
            this.isValid = isValid;
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