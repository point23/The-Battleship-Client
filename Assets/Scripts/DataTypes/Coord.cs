using System;
using GameBase;
using UnityEngine;

namespace DataTypes
{
    [Serializable]
    public class Coord
    {
        public static Coord Zero => new Coord(0, 0);
        public static Coord One => new Coord(1, 1);
        
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

        public override string ToString()
        {
            return ToJson();
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
        
        public static bool operator ==(Coord a, Coord b)
        {
            if (a == null || b == null)
                return false;
            
            return a.Row == b.Row && a.Col == b.Col;
        }
        
        public static bool operator !=(Coord a, Coord b)
        {
            return !(a == b);
        }

        public Vector2 CalculateDelta(Coord other)
        {
            return new Vector2(x - other.x, y - other.y);        
        }
        
        public Vector2Int CalculateDeltaInt(Coord other)
        {
            return new Vector2Int(x - other.x, y - other.y);        
        }
        
        protected bool Equals(Coord other)
        {
            return x == other.x && y == other.y;
        }

        public override bool Equals(object obj)
        {
            return Equals((Coord) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }


        #endregion

    }
}