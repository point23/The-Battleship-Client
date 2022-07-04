using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Utilities;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace DataTypes
{
    [Serializable]
    public struct PolyominoData
    {
        public Coord topLeft;
        public Vector2Int diagonalVector;
        public List<Coord> grids;
        public BoundingBox Bounds => new BoundingBox(diagonalVector);
        public PolyominoData(Coord topLeft, Vector2Int diagonalVector, List<Coord> grids)
        {
            this.topLeft = topLeft;
            this.diagonalVector = diagonalVector;
            this.grids = grids;
        }

        public Vector3 DiagonalVector3 => new Vector3(diagonalVector.x, diagonalVector.y, 0);

        public void RotateClockwiseAround(Vector2 centerPoint)
        {
            topLeft.RotateClockwiseAround(centerPoint, 90);
            RotateDiagonalVectorClockwise();
        }

        public void RotateDiagonalVectorClockwise()
        {
            diagonalVector = new Vector2Int(diagonalVector.y, -diagonalVector.x);
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}