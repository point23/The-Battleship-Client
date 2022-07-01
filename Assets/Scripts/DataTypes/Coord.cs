using System;
using UnityEngine;

namespace DataTypes
{
    [Serializable]
    public class Coord
    {
        public static Coord zero => new Coord(0, 0);
        public static Coord one => new Coord(1, 1);
        
        public int x;
        public int y;
        
        public int Row
        {
            get => x;
            set => x = value;
        }
        
        public int Col
        {
            get => y;
            set => y = value;
        }

        public Vector2 Value
        {
            get => this.ToVector2();
            set
            {
                this.x = (int) (value.x + 0.1);
                this.y = (int) (value.y + 0.1);
            }
        }
        
        public Coord(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public void RotateAroundClockwise(Vector2 center, float angle)
        {
            var delta = center - this.ToVector2();
            Vector2 deltaRotated = Quaternion.Euler(0, 0, angle) * delta;
            // Debug.Log("[Coord] coord" + ToJson() + ", center: " + center + ", delta: " + delta + ", delta1: " + deltaRotated);
            this.Value = (center + deltaRotated);
        }

        #region Convertors

        public Vector2 ToVector2()
        {
            return new Vector2(x, y);
        }

        public Vector2Int ToVector2Int()
        {
            return new Vector2Int(x, y);
        }

        public int ToIndex(int cols)
        {
            return x * cols + y;
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        #endregion


        #region Operators

        public static Coord operator -(Coord coord, Vector2Int delta)
        {
            return new Coord(coord.x - delta.x, coord.y - delta.y);
        }
        
        public static Coord operator +(Coord coord, Vector2Int delta)
        {
            return new Coord(coord.x + delta.x, coord.y + delta.y);
        }

        public static Coord operator -(Coord coord, Vector2 delta)
        {
            return new Coord(coord.x - (int) delta.x, coord.y - (int) delta.y);
        }
        
        public static Coord operator +(Coord coord, Vector2 delta)
        {
            return new Coord(coord.x + (int) delta.x, coord.y + (int) delta.y);
        }

        public Vector2 CalculateDelta(Coord other)
        {
            return new Vector2(x - other.x, y - other.y);        
        }
        
        public Vector2Int CalculateDeltaInt(Coord other)
        {
            return new Vector2Int(x - other.x, y - other.y);        
        }
        
        #endregion

    }
}