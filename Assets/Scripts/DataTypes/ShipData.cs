using System;
using System.Collections.Generic;
using UnityEngine;

namespace DataTypes
{
    [Serializable]
    public struct ShipData
    {
        public Vector2Int boundingBox;
        public Vector2Int topLeft;
        public List<Vector2Int> grids;
        
        public ShipData(Vector2Int boundingBox, Vector2Int topLeft, List<Vector2Int> grids)
        {
            this.boundingBox = boundingBox;
            this.topLeft = topLeft;
            this.grids = grids;
        }
    }
}