using System;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Utilities;
using Vector2 = UnityEngine.Vector2;

namespace DataTypes
{
    [Serializable]
    public struct PolyominoData
    {
        public Coord topLeft;
        public BoundingBox bounds;
        public int angle;
        public List<Coord> gridCoords;

        public PolyominoData(Coord topLeft, BoundingBox bounds, int angle, List<Coord> gridCoords)
        {
            this.topLeft = topLeft;
            this.bounds = bounds;
            this.angle = angle;
            this.gridCoords = gridCoords;
        }
        
        public void RotateClockwiseAround(Vector2 centerPoint)
        {
            topLeft.RotateClockwiseAround(centerPoint, -90);
            RotateDiagonalVectorClockwise();
        }

        public void RotateDiagonalVectorClockwise()
        {
            angle = (angle - 90) % 360;
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}