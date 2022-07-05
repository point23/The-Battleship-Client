using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using Utilities;

namespace DataTypes
{
    [Serializable]
    public class BoundingBox
    {
        public int width;
        public int height;

        public int Max => Math.Max(width, height);
        public int Min => Math.Min(width, height);
        public bool IsSquare() => width == height;

        public BoundingBox(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        
        public BoundingBox(Vector2Int diagonalVector)
        {
            width = Math.Abs(diagonalVector.y) + 1;
            height = Math.Abs(diagonalVector.x) + 1;
        }

        public void Swap()
        {
            (width, height) = (height, width);
        }

        public bool IsCoordIn(Coord coord)
        {
            return  coord.x >= 0 && coord.y >= 0 && coord.x < height && coord.y < width;
        }

        #region Convertors

        public Vector2 ToVector2()
        {
            return new Vector2(height, width);
        }

        public Vector2 ToDiagonalVector()
        {
            return ToVector2() - Vector2.one;
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        public int ConvertCoordToIndex(Coord coord)
        {
            if (IsCoordIn(coord)) 
                return coord.x * width + coord.y;
            
            DebugPG13.LogError(new Dictionary<object, object>()
            {
                {"error", "invalid coord"},
                {"coord", coord.ToJson()},
                {"bounding box", this.ToJson()},
            });
            return -1;
        }

        #endregion
    }
}