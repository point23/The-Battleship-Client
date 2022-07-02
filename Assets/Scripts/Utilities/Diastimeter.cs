using DataTypes;
using GameBase;
using UnityEngine;

namespace Utilities
{
    public class Diastimeter
    {
        private readonly Board _board;
        private Coord _begin;
        private Coord _end;
        public bool IsAvailable => _end.IsInBounds(_board.rows, _board.cols);

        public Diastimeter(Board board)
        {
            _board = board;
        }
        
        public void Begin(Vector3 position)
        {
            _begin = _board.CoordOfPosition(position);
        }

        public Vector2Int CalculateDelta(Vector3 position)
        {
            _end = _board.CoordOfPosition(position);
            return Delta;
        }

        public void End()
        {
            _begin = Coord.zero; 
            _end = Coord.zero;
        }

        public string ToJson()
        {
            return $"begin: {_begin.ToJson()}, end: {_end.ToJson()}";
        }
        
        private Vector2Int Delta => _end.CalculateDeltaInt(_begin);
    }
}