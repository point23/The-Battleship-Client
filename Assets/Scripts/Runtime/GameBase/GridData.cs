using System;
using UnityEngine;

namespace Runtime.GameBase
{
    [Serializable]
    public class GridData
    {
        public Coord coord;
        public bool isActive;
        public bool isValid;

        public GridData(Coord coord, bool isActive = false, bool isValid = true)
        {
            this.coord = coord;
            this.isActive = isActive;
            this.isValid = isValid;
        }
        
        public GridData(int x, int y)
        {
            coord = new Coord(x, y);
        }

        #region Convertors

        public override string ToString()
        {
            return ToJson();
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
        
        #endregion
    }
}