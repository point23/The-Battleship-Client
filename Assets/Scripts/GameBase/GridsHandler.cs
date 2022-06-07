using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameBase
{
    public class GridsHandler : MonoBehaviour
    {
        public GameObject gridTemplate;
        public GridLayoutGroup layoutGroup;
        public int rows;
        public List<Grid> grids;
        private Vector2 CellSize => layoutGroup.cellSize;
        
        public void Awake()
        {
            layoutGroup.cellSize = AppManager.Instance.CellSize;
            grids = new List<Grid>();
            
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < rows; j++)
                {
                    var go = Instantiate(gridTemplate, layoutGroup.transform);
                    grids.Add(go.GetComponent<Grid>());
                }
            }
        }

        public Vector3 GetPosOfGrid(Vector2Int grid)
        {
            var index = grid.x * rows + grid.y;
            if (index < grids.Count && index > 0)
            {
                return grids[index].transform.localPosition;
            }
            
            var topLeft = grids.First().transform.localPosition;
            var offset = (Vector2) (grid - Vector2Int.one);
            var direction = new Vector2(offset.x, -offset.y) * CellSize;
            var pos = topLeft + (Vector3) direction;
            return pos;
        }
    }
}
