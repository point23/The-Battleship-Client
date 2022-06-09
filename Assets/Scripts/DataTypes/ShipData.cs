using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static System.Int32;

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

        public void Rotate()
        {
            boundingBox = new Vector2Int(boundingBox.y, boundingBox.x);
            var maxBound = Math.Max(boundingBox.x, boundingBox.y);
            var center = 0.5f * (maxBound - 1) * Vector2.one;
            for (var i = 0; i < grids.Count; i++)
            {
                grids[i] = RotateGrid(grids[i], center);
            }
        }
        
        public Vector2Int RotateGrid(Vector2Int point, Vector2 center)
        {
            Vector2 result = point;
            // transition
            result -= center;
            // rotate
            result = Quaternion.Euler(0, 0, 90) * result;
            // transition back
            result += center;
            Debug.Log("[ShipData] center: " + center + " , point: " + point + ", result: " + result);
            return new Vector2Int((int) result.x , (int) result.y);
        }
        
        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}