using System.Collections.Generic;
using System.Linq;
using DataTypes;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase
{
    public class GridsHandler : MonoBehaviour
    {
        public GameObject gridTemplate;
        public GridLayoutGroup layoutGroup;
        public int rows = 9;
        public int cols = 9;
        public List<Grid> grids;
        private Vector2 CellSize => layoutGroup.cellSize;
        
        //  *________
        // |__|__|__|
        // |__|__|__|
        // |__|__|__|
        private Vector3 TopLeftPos => grids.First().transform.localPosition;
        
        public void Awake()
        {
            layoutGroup.cellSize = AppManager.Instance.CellSize;
            grids = new List<Grid>();
            
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    var grid = Instantiate(gridTemplate, layoutGroup.transform).GetComponent<Grid>();
                    grid.data = new GridData(i, j);
                    grids.Add(grid);
                }
            }
        }

        public Vector3 GetPosOfGrid(Coord coord)
        {
            var index = coord.ToIndex(cols);
            // Debug.Log("[GridsHandler] coord: " + coord.ToJson() + ", index: " + index);
            if (index < grids.Count && index > 0)
            {
                return grids[index].transform.localPosition;
            }

            var delta = coord.CalculateDelta(Coord.one);
            var displacement = new Vector2(delta.x, -delta.y) * CellSize;
            var pos = TopLeftPos + (Vector3) displacement;
            return pos;
        }
    }
}
