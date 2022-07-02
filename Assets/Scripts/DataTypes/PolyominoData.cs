using System;
using System.Collections.Generic;
using UnityEngine;

namespace DataTypes
{
    [Serializable]
    public struct PolyominoData
    {
        public BoundingBox bounds;
        public Coord topLeft;
        public List<Coord> grids;

        public Vector2 CenterPosition => new(0.5f * (bounds.Max - 1), 0.5f * (bounds.Max - 1));
        public PolyominoData(BoundingBox bounds, Coord topLeft, List<Coord> grids)
        {
            this.bounds = bounds;
            this.topLeft = topLeft;
            this.grids = grids;
        }

        // (0,0)   ___  (0,1)                          *      ___  (0,0)
        //  |            |             rotate          |            |     
        //  | (0.5, 0.5) |             ====>           | (0.5, 0.5) |  
        //  |            |                             |            |     
        //  *      ___  (1,1)                         (1,1)   ___  (0,1) 
        
        // process:
        //                                      (0,0)  ___    *
        //                                       |            | 
        // (0,0)   ___  (0,1)                    | (0.5, 0.5) |     *
        //  |            |         transition    |            |     |
        //  | (0.5, 0.5) |           ====>      (1,1)  ___   (0,1)  |  
        //  |            |                             |            |
        //  *      ___  (1,1)                          *     ___    *
        // result -= center;
        
        //                                       *      ___  (0,0)
        //                                       |            | 
        // (0,0)   ___  (0,1)                    | (0.5, 0.5) |     *
        //  |            |           rotate      |            |     |
        //  | (0.5, 0.5) |           ====>      (1,1)   ___  (0,1)  |
        //  |            |                             |            |
        //  *      ___  (1,1)                          *     ___    *
            
        //  *      ___  (0,0)                           
        //  |            |                             
        //  | (0.5, 0.5) |     *                     *      ___  (0,0)
        //  |            |     |  transition back    |            |   
        // (1,1)   ___  (0,1)  |     ====>           | (0.5, 0.5) |   
        //        |            |                     |            |     
        //        *     ___    *                    (1,1)   ___  (0,1)
        // result += center;
        
        public void Rotate()
        {
            var delta = ResetTopLeft();
            RotateGrids();
            SettleGridsByTopLeftDelta(delta);
            RotateBoundingBox();
        }

        private void RotateGrids()
        {
            foreach (var grid in grids)
            {
                grid.RotateAroundClockwise(CenterPosition, 90);
            }
        }

        private void RotateBoundingBox()
        {
            bounds.Swap();
        }

        private void SettleGridsByTopLeftDelta(Vector2 delta)
        {
            for (var i = 0; i < grids.Count; i++)
            {
                grids[i] -= delta;
            }
        }

        private Vector2 ResetTopLeft()
        {
            if (bounds.IsSquare()) return Vector2Int.zero;
            var oldBottomLeft = new Coord(bounds.height - 1, 0);
            oldBottomLeft.RotateAroundClockwise(CenterPosition, 90);
            var delta = oldBottomLeft.CalculateDelta(Coord.zero);
            return delta;
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}