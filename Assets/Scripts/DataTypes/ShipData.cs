using System.Collections.Generic;
using UnityEngine;

namespace DataTypes
{
    public struct ShipData
    {
        public Vector2 boundingBox;
        public Vector2 bottomLeft;
        public List<Vector2> grids;
        
        public ShipData(Vector2 boundingBox, Vector2 bottomLeft, List<Vector2> grids)
        {
            this.boundingBox = boundingBox;
            this.bottomLeft = bottomLeft;
            this.grids = grids;
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}