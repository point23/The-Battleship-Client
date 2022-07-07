using Runtime.GameBase;
using UnityEngine;

namespace Runtime.Utilities
{
    public class Diastimeter
    {
        private readonly Board _board;
        private Coord _begin;
        private Coord _end;
        public bool IsAvailable => _board.BoundingBox.IsCoordIn(_end);

        public Diastimeter(Board board)
        {
            _board = board;
        }

        public Diastimeter(Diastimeter diastimeter)
        {
            _board = diastimeter._board;
            _begin = diastimeter._begin;
            _end = diastimeter._begin;;
        }

        public Diastimeter()
        {
            
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
            _begin = Coord.Zero; 
            _end = Coord.Zero;
        }

        public string ToJson()
        {
            return $"begin: {_begin.ToJson()}, end: {_end.ToJson()}";
        }
        
        private Vector2Int Delta => _end.CalculateDeltaInt(_begin);
    }
}