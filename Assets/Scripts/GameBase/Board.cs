using System.Collections.Generic;
using System.Linq;
using DataTypes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameBase
{
    public class Board : MonoBehaviour
    {
        public GameObject gridTemplate;
        public GridLayoutGroup gridsLayout;
        public Transform chessTrans;
        public int rows = 9;
        public int cols = 9;
        public List<Grid> grids;

        private Vector2 CellSize
        {
            get => gridsLayout.cellSize;
            set => gridsLayout.cellSize = value;
        }

        public UnityEvent<GridData> onGridClickedEvent;
        
        //  *________
        // |__|__|__|
        // |__|__|__|
        // |__|__|__|
        private Vector3 TopLeftPos => grids.First().transform.localPosition;
        private BoxCollider Collider => GetComponent<BoxCollider>();
        public Bounds Bounds => Collider.bounds;

        private float Height => CellSize.y * rows;
        private float Width => CellSize.x * cols;

        public void Awake()
        {
            CellSize = AppManager.Instance.CellSize;
            grids = new List<Grid>();
            Collider.size = new Vector3(Height, Width);
            Debug.Log("[Board] Awake => bounds:" + Bounds);
        }

        public void GenerateGrids(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    GenerateGrid(i, j);
                }
            }
        }

        public Vector3 LocalPositionOfCoord(Coord coord)
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

        private void GenerateGrid(int i, int j)
        {
            var grid = Instantiate(gridTemplate, gridsLayout.transform).GetComponent<Grid>();
            grid.data = new GridData(i, j);
            grids.Add(grid);
            grid.gridClickedEvent.AddListener(OnGridClicked);
        }
        
        private void OnGridClicked(GridData data)
        {
            onGridClickedEvent.Invoke(data);
        }

    }
}
