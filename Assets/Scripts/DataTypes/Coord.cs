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

        public Coord(Coord other)
        {
            Row = other.Row;
            Col = other.Col;
        }

        public void RotateClockwiseAround(Vector2 center, float angle)
        {
            // .(this) <-delta- .(center)
            var delta = ToVector2() - center;
            //      .(this)
            //      ⬆
            //    delta
            //      |
            //      .(center)
            Vector2 deltaRotated = Quaternion.Euler(0, 0, angle) * delta;
            Value = (center + deltaRotated);
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
            return new Coord(coord.x - Convert.ToInt32(delta.x), coord.y - Convert.ToInt32(delta.y));
        }
        
        public static Coord operator +(Coord coord, Vector2 delta)
        {
            return new Coord(coord.x +  Convert.ToInt32(delta.x), coord.y + Convert.ToInt32(delta.y));
        }

        public Vector2 CalculateDelta(Coord other)
        {
            return new Vector2(x - other.x, y - other.y);        
        }
        
        public Vector2Int CalculateDeltaInt(Coord other)
        {
            return new Vector2Int(x - other.x, y - other.y);        
        }

        public bool IsInBounds(int rows, int cols)
        {
            return  x >= 0 && y >= 0 && x < cols && y < rows;
        }
        
        #endregion

    }
}