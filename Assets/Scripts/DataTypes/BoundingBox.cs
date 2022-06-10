﻿using System;
using UnityEngine;

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

        public BoundingBox(Vector2Int size)
        {
            width = size.x;
            height = size.y;
        }

        public void Swap()
        {
            (width, height) = (height, width);
        }

        public Vector2 ToVector2()
        {
            return new Vector2(height, width);
        }
        
        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

    }
}