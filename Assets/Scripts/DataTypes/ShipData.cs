using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace DataTypes
{
    [Serializable]
    public struct ShipData
    {
        public Vector2Int boundingBox;
        public Vector2Int topLeft;
        public List<Vector2Int> grids;

        private int MaxBound => Math.Max(boundingBox.x, boundingBox.y);
        private Vector2 Center => 0.5f * (MaxBound - 1) * Vector2.one;

        public ShipData(Vector2Int boundingBox, Vector2Int topLeft, List<Vector2Int> grids)
        {
            this.boundingBox = boundingBox;
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
            RotateBoundingBox();
            RotateGrids();
        }

        private void RotateGrids()
        {
            CoordTransformation();
            for (var i = 0; i < grids.Count; i++)
            {
                grids[i] = RotateGrid(grids[i], Center);
            }
            CoordTransformation();
        }

        private void RotateBoundingBox()
        {
            boundingBox = new Vector2Int(boundingBox.y, boundingBox.x);
        }

        public Vector2Int RotateGrid(Vector2Int point, Vector2 center)
        {
            var result = RotatePoint((Vector2) point, center);
            return new Vector2Int((int) (result.x + 0.1), (int) (result.y + 0.1));
        }

        private void CoordTransformation()
        {
            for (var i = 0; i < grids.Count; i++)
            {
                grids[i] = new Vector2Int(grids[i].y, grids[i].x);
            }
        }

        private Vector2 RotatePoint(Vector2 point, Vector2 center)
        {
            var delta = center - point;
            Vector2 delta1 = Quaternion.Euler(0, 0, -90) * delta;
            return (center + delta1);
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}